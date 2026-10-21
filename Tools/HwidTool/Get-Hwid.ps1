<#
Simple PowerShell alternative to collect HW info and compute SHA256 HW hash.
Usage: .\Get-Hwid.ps1 -SaveLocal
#>

param(
	[switch]$SaveLocal,
	[switch]$Json,
	[switch]$LicenseTemplate
)

function QueryFirst($class, $prop) {
	try {
		$obj = Get-CimInstance -ClassName $class | Select-Object -First 1 -ExpandProperty $prop -ErrorAction SilentlyContinue
		return $obj -as [string]
	} catch { return "" }
}

$comp = [ordered]@{
	SystemUuid = QueryFirst 'Win32_ComputerSystemProduct' 'UUID'
	BaseBoardSN = QueryFirst 'Win32_BaseBoard' 'SerialNumber'
	BiosSN = QueryFirst 'Win32_BIOS' 'SerialNumber'
	ProcessorId = QueryFirst 'Win32_Processor' 'ProcessorId'
}

$parts = $comp.Values | Where-Object { -not [string]::IsNullOrWhiteSpace($_) }
$concat = $parts -join '|'
$hw = ([System.BitConverter]::ToString((New-Object -TypeName System.Security.Cryptography.SHA256Managed).ComputeHash([System.Text.Encoding]::UTF8.GetBytes($concat)))) -replace '-', ''

Write-Output "HW_HASH: $hw"
$comp | Get-Member -MemberType NoteProperty | ForEach-Object { $name = $_.Name; Write-Output "$name: $($comp[$name])" }

if ($Json) {
	$out = @{ HwHash = $hw; Components = $comp }
	$out | ConvertTo-Json -Depth 5 | Write-Output
}

if ($SaveLocal) {
	$folder = Join-Path -Path $env:ProgramData -ChildPath 'SmartMRT'
	if (-not (Test-Path $folder)) { New-Item -ItemType Directory -Path $folder -Force | Out-Null }
	$path = Join-Path $folder 'hw_report.json'
	$out = @{ HwHash = $hw; Components = $comp }
	$out | ConvertTo-Json -Depth 5 | Out-File -FilePath $path -Encoding UTF8
	Write-Output "Saved hw_report.json to $path"
}

if ($LicenseTemplate) {
	$payload = @{ ProductCode = 'FFULL_DC'; AllowedModules = 'V_PRODOUTPUT'; MaxLines = 10; IssuedUtc = (Get-Date).ToUniversalTime().ToString('o'); ExpiryUtc = (Get-Date).ToUniversalTime().AddYears(1).ToString('o'); HwHash = $hw }
	$folder = Join-Path -Path $env:ProgramData -ChildPath 'SmartMRT'
	if (-not (Test-Path $folder)) { New-Item -ItemType Directory -Path $folder -Force | Out-Null }
	$licensePath = Join-Path $folder 'license.json'
	$wrapperPath = Join-Path $folder 'license_wrapper.json'
	$payload | ConvertTo-Json -Depth 5 | Out-File -FilePath $licensePath -Encoding UTF8
	$wrapper = @{ LICENSE_PAYLOAD = (Get-Content $licensePath -Raw); SIGNATURE = '' }
	$wrapper | ConvertTo-Json -Depth 5 | Out-File -FilePath $wrapperPath -Encoding UTF8
	Write-Output "Wrote license.json and license_wrapper.json to $folder"
}
