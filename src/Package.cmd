@echo off

SET SolutionDir=%~dp0
SET ConfigurationName=Release

SET TargetName=FishyFish
SET TargetDir=%SolutionDir%GoldFishProject\bin\%ConfigurationName%\
SET TargetPath=%TargetDir%%TargetName%.exe

SET AppXPackageDir=%SolutionDir%AppX\
SET AppXPath=%SolutionDir%%TargetName%.appx
SET AppXPackageName=3652dkackman.FishyFishyFish
SET Win10SDKDir=C:\Program Files (x86)\Windows Kits\10\bin\x64\

:: clean any previous package outputs
rmdir "%AppXPackageDir%Assets" /s /q
rmdir "%SolutionDir%AppX" /s /q
del "%AppXPath%" /q /f

:: create the working folders for packaging
mkdir "%AppXPackageDir%"
mkdir "%AppXPackageDir%Assets\"

:: copy build output, dependencies, and content into the working folder
xcopy "%TargetPath%" "%AppXPackageDir%" /R /Y
xcopy "%TargetPath%.config" "%AppXPackageDir%" /R /Y
xcopy "%TargetDir%Microsoft.HockeyApp.*.dll" "%AppXPackageDir%" /R /Y
xcopy "%TargetDir%Microsoft.Toolkit.Uwp.Notifications.dll" "%AppXPackageDir%" /R /Y
xcopy "%TargetDir%QueryString.NETCore.dll" "%AppXPackageDir%" /R /Y
xcopy "%TargetDir%DesktopBridge.Helpers.dll" "%AppXPackageDir%" /R /Y
xcopy "%TargetDir%DesktopBridgeEnvironment.dll" "%AppXPackageDir%" /R /Y

:: copy appmanifest and assets into the working folder
xcopy "%SolutionDir%Packaging\appxmanifest.xml" "%AppXPackageDir%" /R /Y
xcopy "%SolutionDir%Packaging\Assets\*.*" "%AppXPackageDir%Assets\" /R /Y

:: build the AppX package
"%Win10SDKDir%MakeAppX.exe" pack /d %AppXPackageDir% /p "%AppXPath%"

:: sign the package if the password was passed in as first argument
if "%~1"=="" goto end
"%Win10SDKDir%SignTool.exe" sign -f "%SolutionDir%tempcastore.pfx" -fd SHA256 -p %1 -v "%AppXPath%"

:: install it
powershell if ((Get-AppxPackage -Name '%AppXPackageName%').count -gt 0) { Remove-AppxPackage (Get-AppxPackage -Name '%AppXPackageName%').PackageFullName }
powershell Add-AppxPackage %AppXPath%

goto end

:usage
%0 [SIGNING_CERT_PASSWORD]

:end
