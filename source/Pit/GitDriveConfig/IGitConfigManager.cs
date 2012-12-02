using System.Collections.Generic;

namespace Pit.GitDriveConfig
{
    internal interface IGitConfigManager
    {
        IEnumerable<GitRepository> GetTrackedRepositories();
        GitRepository GetTrackedRepository(string path);
        bool IsTracked(string path);
        void TryCreateConfigFile();
    }
}