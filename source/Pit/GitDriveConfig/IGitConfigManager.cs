using System.Collections.Generic;
using System.Management.Automation;

namespace Pit.GitDriveConfig
{
    internal interface IGitConfigManager
    {
        IEnumerable<GitRepository> GetTrackedRepositories();
        GitRepository GetTrackedRepository(string path);
        bool IsTracked(string path);
        void TryCreateConfigFile();
        void TrackRepo(string name, string path, SwitchParameter newRepository);
    }
}