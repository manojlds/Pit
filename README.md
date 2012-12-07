# Pit #

A git provider for Powershell that helps you to manage git repositories in a machine.

**How to get it?**

You can get it through PsGet.

If not installed, install PsGet

```powershell
(new-object Net.WebClient).DownloadString("http://psget.net/GetPsGet.ps1") | iex
```

Then, install the Pit module:

```powershell
Install-Module Pit
```
or

```powershell
Install-Module -ModuleUrl https://github.com/downloads/manojlds/Pit/Pit.zip
```

If you want it added to the profile, add the `-Startup` flag.

**How to use it?**

You can `cd git:` to manage your tracked repositories.

`ls` would list the repositories tracked and their filesystem paths.

Use `New-Item` to track existing repos or create and track new repos:

```powershell
New-Item git:\ExistingRepo -RepoPath C:\projects\oss\ExistingRepo

New-Item git:\NewRepo -RepoPath C:\projects\oss\NewRepo -New
```

To switch to the filesystem path of a tracked repo, use the `Set-RepoLocation` cmdlet( alias: pcd)

```powershell
pcd -Name NewRepo
```

The above will cd to `C:\projects\oss\NewRepo`