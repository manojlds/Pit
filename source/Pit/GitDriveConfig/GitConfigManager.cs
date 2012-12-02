using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace Pit.GitDriveConfig
{
    class GitConfigManager : IGitConfigManager
    {
        private const string GitDriveConfigFileName = ".gitdrive";

        public string GitDriveConfigFilePath { get; private set; }

        public GitConfigManager()
        {
            GitDriveConfigFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                                              GitDriveConfigFileName);
        }

        public IEnumerable<GitRepository> GetTrackedRepositories()
        {
            using (var configFileStream = new StreamReader(GitDriveConfigFilePath))
            using (var reader = new CsvReader(configFileStream))
            {
                return reader.GetRecords<GitRepository>().ToList();
            }
        }

        public GitRepository GetTrackedRepository(string path)
        {
            return GetTrackedRepositories().FirstOrDefault(repository => repository.Name == path);
        }

        public bool IsTracked(string path)
        {
            return GetTrackedRepository(path) != null;
        }

        public void TryCreateConfigFile()
        {
            if (!File.Exists(GitDriveConfigFilePath))
            {
                File.Create(GitDriveConfigFilePath);
            }
        }
    }
}
