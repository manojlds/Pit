using System.Collections.Generic;
using System.Management.Automation;

namespace Pit.GitDriveConfig
{
    internal interface IGitConfigReader
    {
        IEnumerable<GitRepository> GetTrackedRepositories();
        GitRepository GetTrackedRepository(string path);
        bool IsTracked(string path);
    }
}