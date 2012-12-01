using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Provider;
using Pit.GitDriveConfig;

namespace Pit
{
    [CmdletProvider("Git", ProviderCapabilities.None)]
    public class GitRepositoryProvider : ContainerCmdletProvider
    {
        private readonly IGitConfigReader gitConfigReader;
        private const string PathSeparator = @"\";

        public GitRepositoryProvider()
        {
            gitConfigReader = new GitConfigReader();
        }

        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            return new Collection<PSDriveInfo>
                       {
                           new GitDriveInfo("Git", ProviderInfo, "git:", "Git Provider", null, true)
                       };
        }

        protected override void GetItem(string repo)
        {
            if (PathIsDrive(repo))
            {
                WriteItemObject(PSDriveInfo, repo, true);
                return;
            }
            var trackedRepository = gitConfigReader.GetTrackedRepository(repo);
            if (trackedRepository == null)
            {
                WriteError(new ErrorRecord(
                                   new RepositoryNotTrackedException(repo),
                                   "RepositoryNotTracked",
                                   ErrorCategory.ObjectNotFound,
                                   null));
                return;
            }

            WriteItemObject(trackedRepository, repo, true);
        }

        protected override void GetChildItems(string path, bool recurse)
        {
            if (!PathIsDrive(path)) return;

            foreach (var trackedRepository in gitConfigReader.GetTrackedRepositories())
            {
                WriteItemObject(trackedRepository, path, true);
            }
        }

        protected override void GetChildNames(string path, ReturnContainers returnContainers)
        {
            if (PathIsDrive(path))
            {
                foreach (var trackedRepository in gitConfigReader.GetTrackedRepositories())
                {
                    WriteItemObject(trackedRepository.Name, path, true);
                }
            }
        }

        protected override bool ItemExists(string path)
        {
            if (PathIsDrive(path))
            {
                return true;
            }

            return gitConfigReader.IsTracked(path);
        }

        protected override bool IsValidPath(string path)
        {
            throw new NotImplementedException();
        }

        private bool PathIsDrive(string path)
        {
            return String.IsNullOrEmpty(path.Replace(PSDriveInfo.Root, string.Empty)) ||
                   String.IsNullOrEmpty(path.Replace(PSDriveInfo.Root + PathSeparator, string.Empty));
        }
    }
}
