#requires -Version 5

Param(
    [Parameter(Mandatory=$true)]
    [string]$DllPath
)

$ErrorActionPreference = "Stop"

try {

    Set-Variable -Name "VersionFile" -Value (Join-Path $PSScriptRoot "..\Version.txt") -Option ReadOnly -Force

    # Ensure the DLL exists
    if (-not (Test-Path $DllPath)) {
        throw "DLL does not exist at '$DllPath'.";
    }

    # Get the version number from the DLL
    Write-Host "Obtaining version information from DLL..."
    [System.Diagnostics.FileVersionInfo]$VersionInfo = (Get-Item $DllPath).VersionInfo
    [string]$PluginName = $VersionInfo.FileDescription
    [string]$VersionNumber = $VersionInfo.ProductVersion
    
    Write-Host "Version number for $PluginName is $VersionNumber."

    # Write the version number
    Write-Host "Update version file..."
    $UpdateFileContents = ":`n$($PluginName):$($VersionNumber)`n:"
    $UpdateFileContents | Out-File -FilePath $VersionFile -Encoding utf7
    Write-Host "Version file updated."

} catch {
    throw
}