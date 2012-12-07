using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using CsvHelper;
using Pit.Exceptions;
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
            GitRepositoryManager.CheckIsValidGitPath(path);

            var trackedRepositories = GetTrackedRepositories().ToList();
            var trackedRepository = trackedRepositories.FirstOrDefault(repository =>
                repository.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            if (trackedRepository != null)
            {
                trackedRepository.Path = path;
            } 
            else
            {
                trackedRepositories.Add(new GitRepository{Name = name, Path = path});
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
