using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Pit.GitHelper;

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

        public GitRepository GetTrackedRepository(string name)
        {
            var trackedRepositories = GetTrackedRepositories();
            return GetTrackedRepository(name, trackedRepositories);
        }

        private static GitRepository GetTrackedRepository(string name, IEnumerable<GitRepository> trackedRepositories)
        {
            return trackedRepositories.FirstOrDefault(repository => repository.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public bool IsTracked(string path)
        {
            return GetTrackedRepository(path) != null;
        }

        public void TryCreateConfigFile()
        {
            if (File.Exists(GitDriveConfigFilePath)) return;

            using(var streamWriter = new StreamWriter(GitDriveConfigFilePath))
            using (var csv = new CsvWriter(streamWriter))
            {
                csv.WriteField("Name");
                csv.WriteField("Path");
                csv.NextRecord();
            }
        }

        public void TrackRepo(string name, string path, bool isNewRepository = false)
        {
            if (isNewRepository)
            {
                GitRepositoryManager.CreateRepo(path);
            }
            else
            {
                GitRepositoryManager.CheckIsValidGitPath(path);
            }

            AddRepo(name, path);
        }

        private void AddRepo(string name, string path)
        {
            var trackedRepositories = GetTrackedRepositories().ToList();
            var trackedRepository = GetTrackedRepository(name, trackedRepositories);

            if (trackedRepository != null)
            {
                trackedRepository.Path = path;
            }
            else
            {
                trackedRepositories.Add(new GitRepository {Name = name, Path = path});
            }

            WriteConfig(trackedRepositories);
        }

        private void WriteConfig(IEnumerable<GitRepository> trackedRepositories)
        {
            using (var streamWriter = new StreamWriter(GitDriveConfigFilePath))
            using (var csv = new CsvWriter(streamWriter))
            {
                csv.WriteRecords(trackedRepositories);
            }
        }
    }
}
