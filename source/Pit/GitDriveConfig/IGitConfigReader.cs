using System.Collections.Generic;

namespace Pit.GitDriveConfig
{
    internal interface IGitConfigReader
    {
        IEnumerable<GitRepository> GetTrackedRepositories();
        GitRepository GetTrackedRepository(string path);
        bool IsTracked(string path);
    }
}