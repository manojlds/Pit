using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace Pit.GitDriveConfig
{
    class GitConfigReader : IGitConfigReader
    {
        private const string GitDriveConfigFileName = ".gitdrive";

        protected string GitDriveConfigFilePath { get; set; }

        public GitConfigReader()
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
    }
}
