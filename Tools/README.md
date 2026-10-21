# SmartMRT Tools

This folder contains utilities to collect HW information (HWID) and to create signed license payloads for SmartMRT.

Files
- HwidTool: collects HW components and produces an HW hash. Can save to %ProgramData%\SmartMRT.
- Get-Hwid.ps1: PowerShell alternative to collect HW data (no .NET runtime required).
- SignerTool: .NET 8 signing helper (admin-side).
- SignerToolNet472: .NET Framework 4.7.2 signing helper (admin-side, uses BouncyCastle).
- sign_and_prepare_db.ps1: PowerShell helper to automate signing and produce a SQL insert.

Typical workflow
1. On target machine (installer or technician) collect HW info:
   - PowerShell (recommended for customer machines):
	 ```powershell
	 .\Tools\HwidTool\Get-Hwid.ps1 -SaveLocal -Json -LicenseTemplate
	 ```
	 This writes files to: %ProgramData%\SmartMRT\hw_report.json and license templates.

   - Or run the console HWID tool (if .NET 8 runtime present):
	 ```powershell
	 dotnet run --project Tools/HwidTool -- --save-local --license-template --print
	 ```

2. Transfer hw_report.json (or HW_HASH) to your admin machine.

3. Create payload.json (exact text matters): example payload.json
   ```json
   {
	 "ProductCode":"FSTANDARD_DC",
	 "AllowedModules":"V_PRODOUTPUT",
	 "MaxLines":5,
	 "IssuedUtc":"2026-09-03T12:00:00Z",
	 "ExpiryUtc":"2027-09-03T12:00:00Z",
	 "HwHash":"<hw_hash_here>"
   }
   ```

4. Sign the payload (admin machine) using either SignerToolNet472.exe or SignerTool (net8):
   - If you have .NET Framework only (recommended for some admin PCs):
	 - Build the project in Visual Studio, then run:
	   ```powershell
	   .\Tools\SignerToolNet472\bin\Debug\net472\SignerToolNet472.exe --in C:\temp\payload.json --privatekey C:\keys\privatekey.pem --out C:\temp
	   ```
   - If you have .NET 8 SDK/runtime installed:
	 ```powershell
	 dotnet run --project Tools/SignerTool -- --in C:\temp\payload.json --privatekey C:\keys\privatekey.pem --out C:\temp
	 ```
   - Output: base64 signature printed and file C:\temp\license_wrapper.json containing LICENSE_PAYLOAD and SIGNATURE.

5. Install the license on the target machine
   - Copy publickey.pem to: %ProgramData%\SmartMRT\publickey.pem
   - Copy license_wrapper.json (or license.json) to: %ProgramData%\SmartMRT\license.json
   - Or insert LICENSE_PAYLOAD and SIGNATURE into DC_SETUP table (example SQL below).

SQL template (example)
```
INSERT INTO DASHBOARD_GLOBALDB.dbo.DC_SETUP
  (COMPANY_NAME, SIGNATURE, LICENSE_TYPE, LICENSE_KEY, DATEMODIFIED, HW_HASH, LICENSE_PAYLOAD, ALLOWED_MODULES, MAX_LINES, ISSUED_UTC, EXPIRY_UTC, IS_ACTIVE)
VALUES
  ('CustomerCo', '<base64-signature>', 'FULL', NULL, GETUTCDATE(), '<hwHash>',
   '<LICENSE_PAYLOAD JSON string here>', 'V_PRODOUTPUT', 5,
   '2026-09-03T12:00:00Z', '2027-09-03T12:00:00Z', 1);
```

Notes
- LICENSE_PAYLOAD bytes must match exactly what was signed. Avoid reformatting JSON when storing in DB.
- Keep private keys secure. Only publickey.pem is placed on customer machines.
- If you need a single-file, self-contained signer executable for admin machines, consider publishing the net8 SignerTool as self-contained (win-x64 or win-x86).
