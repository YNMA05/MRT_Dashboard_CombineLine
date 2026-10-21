
#define BuildRoot "E:\Source Code\SmartMRT Dashboard\Dashboard - Combine Line\SmartMRT_DashCombine"

[Setup]
Password=SQ91Y3443
AppId={{A6E84F21-9C34-4B76-A125-73D8F2A90C41}
AppName=SmartMRT Dashboard
AppVersion=1.0.0
DefaultDirName={autopf}\SmartMRT_Dashboard
DefaultGroupName=SmartMRT Dashboard
PrivilegesRequired=admin
OutputDir={#BuildRoot}\Installer\Output
OutputBaseFilename=SmartMRT_DashBoardInstaller
ArchitecturesInstallIn64BitMode=x64
Uninstallable=yes
SetupIconFile={#BuildRoot}\Installer\SmartMRT_Dashboard.ico
UninstallDisplayIcon={app}\Manager\image_f9de2a5b.ico
CloseApplications=yes

[Dirs]
Name: "{commonappdata}\SmartMRT_Dashboard\Config"
Name: "{commonappdata}\SmartMRT_Dashboard\License"
Name: "{commonappdata}\SmartMRT_Dashboard\Logs"
Name: "{commonappdata}\SmartMRT_Dashboard\StoredProcedure"

[Files]
; WinForms Manager and its dependencies
Source: "{#BuildRoot}\Setup_SmartMRT_DashCombine\SmartMRT_DashCombine_Setup\bin\Release\*"; \
    DestDir: "{app}\Manager"; \
    Flags: ignoreversion recursesubdirs createallsubdirs

; Published .NET service and React dashboard
Source: "{#BuildRoot}\Service_SmartMRT_DashCombine\publish\*"; \
    DestDir: "{app}\Service"; \
    Flags: ignoreversion recursesubdirs createallsubdirs

; WinForms image assets
Source: "{#BuildRoot}\Setup_SmartMRT_DashCombine\PictureModule\*"; \
    DestDir: "{app}\Combine"; \
    Flags: ignoreversion recursesubdirs createallsubdirs
; Stored procedure assets
Source: "C:\ProgramData\SmartMRT_Dashboard\StoredProcedure\*"; \
    DestDir: "{commonappdata}\SmartMRT_Dashboard\StoredProcedure"; \
    Flags: ignoreversion onlyifdoesntexist
    
; Public license verification key only
Source: "C:\ProgramData\SmartMRT_Dashboard\License\publickey.pem"; \
    DestDir: "{commonappdata}\SmartMRT_Dashboard\License"; \
    Flags: ignoreversion onlyifdoesntexist

[Tasks]
Name: "desktopicon"; \
    Description: "Create a Desktop shortcut"; \
    GroupDescription: "Additional shortcuts:"; \
    Flags: unchecked

[Icons]
Name: "{autoprograms}\SmartMRT Dashboard Manager"; \
    Filename: "{app}\Manager\SmartMRT_DashCombine_Setup.exe"; \
    WorkingDir: "{app}\Manager"; \
    IconFilename: "{app}\Manager\image_f9de2a5b.ico"
  
;  Desktop shortcut
Name: "{autodesktop}\SmartMRT Dashboard Manager"; \
    Filename: "{app}\Manager\SmartMRT_DashCombine_Setup.exe"; \
    WorkingDir: "{app}\Manager"; \
    IconFilename: "{app}\Manager\image_f9de2a5b.ico"; \
    Tasks: desktopicon

[Run]
Filename: "{app}\Manager\SmartMRT_DashCombine_Setup.exe"; \
    Description: "Open SmartMRT Dashboard Manager"; \
    Flags: postinstall nowait skipifsilent