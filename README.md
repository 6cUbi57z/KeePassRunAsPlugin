# KeePass RunAs Plugin #

- [Introduction](#introduction)
- [Requirements](#requirements)
- [Contributing](#contributing)
    - [Development Requirements](#development-requirements)
    - [Environment Setup](#environment-setup)
    - [Debugging](#debugging)
    - [Submitting Changes](#submitting-changes)
- [Special Thanks](#special-thanks)

## Introduction ##

This is the repository for the Run As plugin for the [KeePass Password Safe](https://keepass.info/). The plugin adds functionality to easily execute an application using the credentials stored in a password entry.

## Requirements ##

This plugin has the following requirements:

- A Windows Operating System
- KeePass 2.40
- .NET Framework 4.6

There are currently no plans to support operating systems other than Windows but if this is required, feel free to [raise an issue](https://github.com/dale2507/KeePassRunAsPlugin/issues/new).

The limitation of requiring KeePass 2.40 will be resolved with [Issue 1](https://github.com/dale2507/KeePassRunAsPlugin/issues/1).

## Contributing ##

### Development Requirements ###

In order to develop the plugin, you will need to meet the runtime requirments above. You will also require:

- Visual Studio 2017 (Older versions may work but have not been tested).
- PowerShell 5 or a copy of [KeePass 2.40 Portable](https://sourceforge.net/projects/keepass/files/KeePass%202.x/2.40/KeePass-2.40.zip/download).

It may also be useful to download the KeePass Source Code for reference.

### Environment Setup ###

The only environment setup should be to ensure that KeePass 2.40 Portable is in the correct location for the solution to reference. To ease environment setup, a script has been provided to automate this. To execute this, use the following steps:

1. Open the solution file (`src\KeePassRunAsPlugin.sln`) in Visual Studio.
2. Open the Package Manager Console (Tools -> NuGet Package Manager -> Package Manager Console).
3. Enter the following command and hit `Enter`:
    ```powershell
    ..\build\Get-KeePassLib.ps1
    ```

Alternatively, you can download the portable version manually and extract it so that `KeePass.exe` is at `<RepositoryRoot>\lib\KeePass\KeePass.exe`.

### Debugging ###

**TL;DR;** Press F5

The project is configured so that, after a successful build, the DLL is copied to the KeePass download in the `lib\KeePass` folder. This allows the plugin to be tested by executing `KeePass.exe` in this folder. The project is configured to do this with the debugger attached so that you can debug in the same way as any other C# application.

### Submitting Changes ###

TBC

## Special Thanks ##

- Dominik Reichl, creator of KeePass.
- [Rafael Cossovan](https://github.com/navossoc), developer of [Yet Another Favicon Downloader](https://github.com/navossoc/KeePass-Yet-Another-Favicon-Downloader) - I used his code to work out how to update password entry icons.
- [Mitch Capper](https://github.com/mitchcapper), developer of [KPEntryTemplates](https://github.com/mitchcapper/KPEntryTemplates) - I used his code to work out how to modify the Password Entry form.