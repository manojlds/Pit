using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using CsvHelper;

namespace Pit.GitDriveConfig
{
    class GitConfigReader : IGitConfigReader
    {
        public IEnumerable<GitRepository> GetTrackedRepositories(PSDriveInfo psDriveInfo)
        {
            var gitDriveInfo = psDriveInfo as GitDriveInfo;
            if (gitDriveInfo == null) return Enumerable.Empty<GitRepository>();

            using (var configFileStream = new StreamReader(gitDriveInfo.GitDriveConfigFile))
            using (var reader = new CsvReader(configFileStream))
            {
                return reader.GetRecords<GitRepository>().ToList();
            }
        }

        public GitRepository GetTrackedRepository(PSDriveInfo psDriveInfo, string path)
        {
            return GetTrackedRepositories(psDriveInfo).FirstOrDefault(repository => repository.Name == path);
        }

        public bool IsTracked(PSDriveInfo psDriveInfo, string path)
        {
            return GetTrackedRepository(psDriveInfo, path) != null;
        }
    }
}
