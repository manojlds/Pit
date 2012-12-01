using System.Collections.Generic;
using System.Management.Automation;

namespace Pit.GitDriveConfig
{
    internal interface IGitConfigReader
    {
        IEnumerable<GitRepository> GetTrackedRepositories(PSDriveInfo psDriveInfo);
        GitRepository GetTrackedRepository(PSDriveInfo psDriveInfo, string path);
        bool IsTracked(PSDriveInfo psDriveInfo, string path);
    }
}