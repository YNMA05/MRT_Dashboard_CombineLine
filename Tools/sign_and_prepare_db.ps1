<#
PowerShell helper to sign payload and produce a SQL insert for DC_SETUP.

Usage:
  .\sign_and_prepare_db.ps1 -Payload C:\temp\payload.json -PrivateKey C:\keys\privatekey.pem -OutDir C:\temp

Inputs:
- Payload: path to payload.json (must contain HwHash)
- PrivateKey: path to PEM private key
- HwReport: optional path to hw_report.json to extract HwHash
- OutDir: where to write license_wrapper.json and insert SQL

#>

	[string]$Payload,
	[string]$PrivateKey,
	[string]$HwReport,
	[string]$OutDir = "$pwd",
	[switch]$UseNet8


if (-not $Payload -and -not $HwReport) {
	Write-Error "Provide -Payload or -HwReport"
	exit 2
}

if (-not (Test-Path $PrivateKey)) {
	Write-Error "Private key not found: $PrivateKey"
	exit 2
}

if ($HwReport) {
	$hr = Get-Content $HwReport -Raw | ConvertFrom-Json
	$hwHash = $hr.HwHash
	if (-not $Payload) {
		# create basic payload template if not provided
		$pl = @{ ProductCode = 'TBASIC_DC'; AllowedModules = 'V_PRODOUTPUT'; MaxLines = 2; IssuedUtc = (Get-Date).ToUniversalTime().ToString('o'); ExpiryUtc = (Get-Date).ToUniversalTime().AddYears(1).ToString('o'); HwHash = $hwHash }
		$Payload = Join-Path $OutDir 'payload.json'
		$pl | ConvertTo-Json -Depth 5 | Out-File -FilePath $Payload -Encoding UTF8
		Write-Output "Wrote payload template to $Payload"
	} else {
		# ensure payload contains HwHash
		$txt = Get-Content $Payload -Raw
		if ($txt -notmatch '"HwHash"') {
			$obj = $txt | ConvertFrom-Json
			$obj.HwHash = $hwHash
			$obj | ConvertTo-Json -Depth 5 | Out-File -FilePath $Payload -Encoding UTF8
			Write-Output "Inserted HwHash into $Payload"
		}
	}
}

# choose signer
$net472Exe = Join-Path -Path (Join-Path -Path $PSScriptRoot -ChildPath 'SignerToolNet472\bin\Debug\net472') -ChildPath 'SignerToolNet472.exe'
$outDirFull = Resolve-Path -Path $OutDir
New-Item -ItemType Directory -Path $outDirFull -Force | Out-Null

if (-not $UseNet8 -and (Test-Path $net472Exe)) {
	Write-Output "Using SignerToolNet472 at $net472Exe"
	& $net472Exe --in $Payload --privatekey $PrivateKey --out $outDirFull
} else {
	Write-Output "Using dotnet SignerTool (net8)"
	dotnet run --project "$PSScriptRoot/SignerTool" -- --in $Payload --privatekey $PrivateKey --out $outDirFull
}

# locate wrapper JSON
$wrapperPath = Join-Path $outDirFull 'license_wrapper.json'
if (-not (Test-Path $wrapperPath)) {
	Write-Error "license_wrapper.json not found in $outDirFull"
	exit 3
}

$wrapperText = Get-Content $wrapperPath -Raw
$wrapper = $wrapperText | ConvertFrom-Json

# prepare SQL insert (escape single quotes)
$payloadEscaped = $wrapper.LICENSE_PAYLOAD -replace "'", "''"
$signature = $wrapper.SIGNATURE
$hw = ($wrapper.LICENSE_PAYLOAD | ConvertFrom-Json).HwHash

$sql = @"
INSERT INTO DASHBOARD_GLOBALDB.dbo.DC_SETUP
  (COMPANY_NAME, SIGNATURE, LICENSE_TYPE, LICENSE_KEY, DATEMODIFIED, HW_HASH, LICENSE_PAYLOAD, ALLOWED_MODULES, MAX_LINES, ISSUED_UTC, EXPIRY_UTC, IS_ACTIVE)
VALUES
  ('CustomerCo', '$signature', 'FULL', NULL, GETUTCDATE(), '$hw',
   '$payloadEscaped', 'V_PRODOUTPUT', $((($wrapper.LICENSE_PAYLOAD | ConvertFrom-Json).MaxLines)),
   '$((($wrapper.LICENSE_PAYLOAD | ConvertFrom-Json).IssuedUtc))', '$((($wrapper.LICENSE_PAYLOAD | ConvertFrom-Json).ExpiryUtc))', 1);
"@

$sqlPath = Join-Path $outDirFull 'insert_license.sql'
$sql | Out-File -FilePath $sqlPath -Encoding UTF8
Write-Output "Wrote SQL insert to $sqlPath"
Write-Output "Done. Copy publickey.pem to %ProgramData%\SmartMRT\publickey.pem and license_wrapper.json or run the SQL to insert." 
