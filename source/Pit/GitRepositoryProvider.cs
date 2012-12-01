using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Provider;

namespace Pit
{
    [CmdletProvider("Git", ProviderCapabilities.None)]
    public class GitRepositoryProvider : ContainerCmdletProvider
    {
        private const string PathSeparator = @"\";

        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            return new Collection<PSDriveInfo>
                       {
                           new GitDriveInfo("Git", ProviderInfo, "git:", "Git Provider", null, true)
                       };
        }

        protected override void GetItem(string path)
        {
            if (PathIsDrive(path))
            {
                WriteItemObject(PSDriveInfo, path, true);
                return;
            }
        }

        protected override bool ItemExists(string path)
        {
            if (PathIsDrive(path))
            {
                return true;
            }

            return false;
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
