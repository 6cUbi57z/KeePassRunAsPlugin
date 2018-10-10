#requires -Version 5

try {

    Set-Variable -Name DOWNLOAD_REFERRER -Value "https://sourceforge.net/projects/keepass/files/KeePass%202.x/2.40/KeePass-2.40.zip/download?use_mirror=netcologne&r=https%3A%2F%2Fsourceforge.net%2Fprojects%2Fkeepass%2Ffiles%2FKeePass%25202.x%2F2.40%2FKeePass-2.40.zip%2Fdownload"
    Set-Variable -Name DOWNLOAD_URL -Value "https://netix.dl.sourceforge.net/project/keepass/KeePass%202.x/2.40/KeePass-2.40.zip" -Option ReadOnly -Force

    Set-Variable -Name KEEPASS_LIB_PATH -Value "$PSScriptRoot\lib\KeePass" -Option ReadOnly -Force
    Set-Variable -Name DOWNLOAD_FILENAME -Value "Download.zip" -Option ReadOnly -Force

    Set-Variable -Name USER_AGENT -Value "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36" -Option ReadOnly -Force

    # Delete the KeePass lib directory so that we start fresh
    if (Test-Path $KEEPASS_LIB_PATH) {
        Write-Output "Deleting KeePass lib directory..."
        Remove-Item $KEEPASS_LIB_PATH -Recurse
    }

    # Create the KeePass lib directory
    Write-Output "Creating lib directory for KeePass Portable..."
    New-Item -Path $KEEPASS_LIB_PATH -ItemType Directory | Out-Null

    # Download the portable version of KeePass
    Write-Output "Downloading KeePass Portable..."
    $zipPath = Join-Path $KEEPASS_LIB_PATH $DOWNLOAD_FILENAME
    Invoke-WebRequest -Uri $DOWNLOAD_URL -UserAgent $USER_AGENT -Headers @{ Referrer = $DOWNLOAD_REFERRER } -OutFile $zipPath

    # Extract the zip file
    Write-Output "Extracting download..."
    Expand-Archive -Path $zipPath -OutputPath $KEEPASS_LIB_PATH

    # Delete the download file to clean up
    Write-Output "Cleaning up..."
    Remove-Item $zipPath

    Write-Output "Done!"
} catch {
    throw
}