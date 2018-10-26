# KeePass Impersonation Plugin #

The KeePass Impersonation Plugin (aka KeePass RunAs Plugin) was developed to help in running applications as a user who's credentials are stored in your KeePass Password Safe.

The main use cases for this are:

* network admins who may need to run some applications as privileged users or,
* VPN users who's local account details are different from those they need to use to connect. [More Details...](UseCaseVPN.md)

## Requirements ##

This plugin has the following requirements:

* A Windows Operating System
* KeePass 2.40 or higher
* .NET Framework 4.6

There are currently no plans to support operating systems other than Windows but if this is required, feel free to [raise an issue](https://github.com/dale2507/KeePassRunAsPlugin/issues/new).

## Setup ##

There is only one configuration option for the plugin at the moment which is to have one password entry per application. You can learn about this on the [Setup Instructions Page](SetupEntryPerApplication.md).

## Special Thanks ##

See the [thanks page](../THANKS.md).