# Pit #

A git provider for Powershell that helps you to manage and query git repositories in a machine.

**How to get it?**

Get it throught Nuget:

```powershell
Install-Package Pit

```

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

The above will cd to `C:\projects\oss\NewRepo`.

You can use `Get-Content` to read the README.md of a project

```powershell
Get-Content git:\pit
gc git:\pit
```

You can use `Get-GitStatus` and `Get-GitLog` cmdlets to query the status and log of your repositories respectively.