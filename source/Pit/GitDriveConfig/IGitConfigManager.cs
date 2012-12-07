using System.Collections.Generic;

namespace Pit.GitDriveConfig
{
    internal interface IGitConfigManager
    {
        IEnumerable<GitRepository> GetTrackedRepositories();
        GitRepository GetTrackedRepository(string name);
        bool IsTracked(string path);
        void TryCreateConfigFile();
        void TrackRepo(string name, string path, bool isNewRepository = false);
    }
}