using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Provider;
using Pit.Exceptions;
using Pit.GitDriveConfig;

namespace Pit.GitRepositoryProvider
{
    [CmdletProvider("Git", ProviderCapabilities.None)]
    public class GitRepositoryProvider : ContainerCmdletProvider, IContentCmdletProvider
    {
        private readonly IGitConfigManager gitConfigManager;
        private const string PathSeparator = @"\";

        public GitRepositoryProvider()
        {
            gitConfigManager = new GitConfigManager();
        }

        protected override bool IsValidPath(string path)
        {
            return !path.Contains(PathSeparator);
        }

        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            return new Collection<PSDriveInfo>
                       {
                           new GitDriveInfo("git", ProviderInfo, "git:", "Git Provider", null, true)
                       };
        }

        protected override ProviderInfo Start(ProviderInfo providerInfo)
        {
            gitConfigManager.TryCreateConfigFile();
            return ProviderInfo;
        }

        protected override bool ItemExists(string path)
        {
            if (PathIsDrive(path))
            {
                return true;
            }

            return gitConfigManager.IsTracked(path);
        }

        protected override void NewItem(string path, string itemTypeName, object newItemValue)
        {
            var newItemParameters = DynamicParameters as NewItemParameters;

            if (newItemParameters == null || newItemParameters.RepoPath == null)
            {
                throw new ArgumentException("Expected RepoPath to be specified");
            }

            gitConfigManager.TrackRepo(path, newItemParameters.RepoPath, newItemParameters.New);
        }

        protected override object NewItemDynamicParameters(string path, string itemTypeName, object newItemValue)
        {
            return new NewItemParameters();
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
            if (trackedRepository != null)
            {
                WriteItemObject("Git repo", path, false);
            }
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

        protected override bool HasChildItems(string path)
        {
            if (PathIsDrive(path)) return true;

            return gitConfigManager.IsTracked(path);
        }

        private static bool PathIsDrive(string path)
        {
            return String.IsNullOrEmpty(path);
        }

        public IContentReader GetContentReader(string path)
        {
            if (PathIsDrive(path)) return null;
            var trackedRepository = gitConfigManager.GetTrackedRepository(path);

            return trackedRepository != null ? new ReadmeContentReader(trackedRepository.Path) : null;
        }

        public object GetContentReaderDynamicParameters(string path)
        {
            return null;
        }

        public IContentWriter GetContentWriter(string path)
        {
            throw new NotImplementedException();
        }

        public object GetContentWriterDynamicParameters(string path)
        {
            throw new NotImplementedException();
        }

        public void ClearContent(string path)
        {
            throw new NotImplementedException();
        }

        public object ClearContentDynamicParameters(string path)
        {
            throw new NotImplementedException();
        }
    }
}
