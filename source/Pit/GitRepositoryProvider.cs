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
        private readonly IGitConfigManager gitConfigManager;
        private NewItemParamters newItemParameters;
        private const string PathSeparator = @"\";

        public GitRepositoryProvider()
        {
            gitConfigManager = new GitConfigManager();
        }

        protected override ProviderInfo Start(ProviderInfo providerInfo)
        {
            gitConfigManager.TryCreateConfigFile();
            return ProviderInfo;
        }

        protected override void NewItem(string path, string itemTypeName, object newItemValue)
        {
            WriteDebug(newItemParameters.RepoPath);
        }

        protected override object NewItemDynamicParameters(string path, string itemTypeName, object newItemValue)
        {
            return newItemParameters = newItemParameters ?? new NewItemParamters();
        }

        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            return new Collection<PSDriveInfo>
                       {
                           new GitDriveInfo("git", ProviderInfo, "git:", "Git Provider", null, true)
                       };
        }

        protected override bool HasChildItems(string path)
        {
            if (PathIsDrive(path)) return true;

            return gitConfigManager.IsTracked(path);
        }

        protected override void GetItem(string repo)
        {
            if (PathIsDrive(repo))
            {
                WriteItemObject(PSDriveInfo, repo, true);
                return;
            }
            var trackedRepository = gitConfigManager.GetTrackedRepository(repo);
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
            if (PathIsDrive(path))
            {
                foreach (var repository in gitConfigManager.GetTrackedRepositories())
                {
                    WriteItemObject(repository, path, true);
                }
                return;
            }
            var trackedRepository = gitConfigManager.GetTrackedRepository(path);
            if (trackedRepository != null)
            {
                WriteItemObject("Git repo", path, false);
            }
            
        }

        protected override void GetChildNames(string path, ReturnContainers returnContainers)
        {
            if (PathIsDrive(path))
            {
                foreach (var repository in gitConfigManager.GetTrackedRepositories())
                {
                    WriteItemObject(repository.Name, path, true);
                }
                return;
            }
            var trackedRepository = gitConfigManager.GetTrackedRepository(path);
            if (trackedRepository !=null)
            {
                WriteItemObject("Git repo", path, false);
            }
        }

        protected override bool ItemExists(string path)
        {
            if (PathIsDrive(path))
            {
                return true;
            }

            return gitConfigManager.IsTracked(path);
        }

        protected override bool IsValidPath(string path)
        {
            return !path.Contains(PathSeparator);
        }

        private bool PathIsDrive(string path)
        {
            return String.IsNullOrEmpty(path);
        }
    }
}
