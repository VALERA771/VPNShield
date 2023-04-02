![Github All Downloads](https://img.shields.io/github/downloads/VALERA771/VPNShield/total.svg?style=flat)

# VPNShield EXILED Edition
VPNShield EXILED Edition - A VPN blocking plugin for EXILED SCPSL servers.<br><br>
Updated version of VPNShield (originally made by SomewhatSane)

<h1>Installation</h1>

<p>Unzip archive from releases to C:\<Yor User>\AppData\Roaming\EXILED\Plugins.

<h1>Configuration</h1>

<p>After starting up the plugin for the first time, a folder named VPNShield will be created in your plugins folder which contains whitelists and blacklists that VPNShield uses for caching known good and bad IPs / accounts. This folder will contain 4 files:

- `Data.db`: Contains all information. Cannot be edited manually, use commands which are listed below

<h1>Commands</h1>

|Command|Description|Requirered Permission|
|---------|--------------|----------------|
|`vs_blacklistip`|Blacklist an IP address|VPNShield.blacklistip|
|`vs_blacklistipsubnet`|Blacklist an IP address subnet. Expects CIDR notation|VPNShield.blacklistip.subnet|
|`vs_clearips`|Clear all IP addresses from VPNShield's database|VPNShield.clear.ips|
|`vs_clearsubnets`|Clear all IP subnets from VPNShield's database|VPNShield.clear.subnets|
|`vs_clearuserids`|Clear all user IDs from VPNShield's database|VPNShield.clear.userids|
|`vs_getstatus`|Get information that VPNShield has on an IP address or User ID|VPNShield.get.status|
|`vs_getwhitelistedsubnets`|Get a list of subnets that have been whitelisted|VPNShield.get.whitelistedsubnets|
|`vs_whitelist`|Exempt players from VPNShield checks|VPNShield.whitelist|
|`vs_whitelistip`|Whitelist an IP address|VPNShield.whitelist.ip|
|`vs_whitelistipsubnet`|Whitelist an IP address subnet. Expects CIDR notation|VPNShield.whitelist.ipsubnet|

For commands which are requering ID you can use: `STEAMID64@steam`, `DISCORDID@discord`, `staffmember@nothwood`.

<h1>Support</h1>

If you have any problems, you can contact me on Discord (VALERA771#1471)
