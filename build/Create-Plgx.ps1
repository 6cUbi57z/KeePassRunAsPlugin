#requires -Version 5

$ErrorActionPreference = "Stop"

try {

    Set-Variable -Name "REPOSITORY_ROOT" -Value (Join-Path $PSScriptRoot "..\") -Option ReadOnly -Force
    Set-Variable -Name "KEEPASS_EXECUTABLE" -Value (Join-Path $REPOSITORY_ROOT "lib\KeePass\KeePass.exe") -Option ReadOnly -Force
    Set-Variable -Name "PROJECT_DIR" -Value (Join-Path $REPOSITORY_ROOT "src\RunAsPlugin") -Option ReadOnly -Force
    Set-Variable -Name "TEMP_BUILD_DIR_NAME" -Value "publish" -Option ReadOnly -Force
    Set-Variable -Name "TEMP_BUILD_DIR_FULL_PATH" -Value (Join-Path $REPOSITORY_ROOT "publish") -Option ReadOnly -Force
    Set-Variable -Name "EXCLUDE_FILE" -Value (Join-Path $PSScriptRoot "plgx-exclude.txt") -Option ReadOnly -Force
    Set-Variable -Name "PLGX_FILE" -Value (Join-Path $REPOSITORY_ROOT "RunAsPlugin.plgx") -Option ReadOnly -Force

    Set-Variable -Name "REQUIREMENT_KEEPASS_VERSION" -Value "2.40" -Option ReadOnly -Force
    Set-Variable -Name "REQUIREMENT_DOTNET_VERSION" -Value "4.6" -Option ReadOnly -Force
    Set-Variable -Name "REQUIREMENT_OS" -Value "Windows" -Option ReadOnly -Force

    # Remove the temporary build directory to use for storing the files for the plgx build if it already exists.
    if (Test-Path $TEMP_BUILD_DIR_FULL_PATH) {
        Write-Host "Temporary build directory '$TEMP_BUILD_DIR_FULL_PATH' already exists. Deleting..."
        Remove-Item $TEMP_BUILD_DIR_FULL_PATH -Recurse
    }

    # Create a temporary directory to use for storing the files for the plgx build.

    Write-Host "Creating temporary build directory '$TEMP_BUILD_DIR_FULL_PATH'..."
    New-Item -Path $TEMP_BUILD_DIR_FULL_PATH -ItemType Directory | Out-Null

    # Copy the project files to the temporary directory so that we don't have to remove bin, obj and user files.
    # Need to use xcopy as the exclude parameter on Copy-Item doesn't work for directories.
    Write-Host "Copying project files to temporary build directory '$TEMP_BUILD_DIR_FULL_PATH'..."
    xcopy "$PROJECT_DIR" "$TEMP_BUILD_DIR_FULL_PATH\" /s /e "/exclude:$EXCLUDE_FILE"

    # Run the plgx build.
    Write-Host "Building plgx..."

    # Make sure we resolve all of the paths, otherwise the paths in the PLGX file can end up absolute or including "..\" which messes up compilation.
    $resolveKeePassExecutable = Join-Path $KEEPASS_EXECUTABLE "." -Resolve
    $resolvedBuildPath = Join-Path $TEMP_BUILD_DIR_FULL_PATH "." -Resolve

    &"$resolveKeePassExecutable" --plgx-create "$resolvedBuildPath" "--plgx-prereq-kp:$REQUIREMENT_KEEPASS_VERSION" "--plgx-prereq-net:$REQUIREMENT_DOTNET_VERSION" "--plgx-prereq-os:$REQUIREMENT_OS"
    if ($LASTEXITCODE -ne 0) {
        throw "PLGX build returned a non-zero exit code. This usually indicates an error."
    }

    # Have to sleep for a second to let KeePass/FileSystem catch up.
    Start-Sleep -Seconds 1

    # Rename the plgx file.
    Write-Host "Renaming plgx file to '$PLGX_FILE'..."
    if (Test-Path $PLGX_FILE) {
        Remove-Item -Path "$PLGX_FILE"
    }
    $pwd = Get-Location
    Set-Location $REPOSITORY_ROOT
    Move-Item -Path "$TEMP_BUILD_DIR_NAME.plgx" -Destination "$PLGX_FILE"
    Set-Location $pwd

    # Delete the temporary directory.
    Write-Host "Removing temporary build directory '$TEMP_BUILD_DIR_FULL_PATH'..."
    Remove-Item -Path $TEMP_BUILD_DIR_FULL_PATH -Recurse

} catch {
    throw
}