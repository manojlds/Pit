using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Provider;

namespace Pit
{
    [CmdletProvider("Git", ProviderCapabilities.None)]
    public class GitRepositoryProvider : ContainerCmdletProvider
    {
        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            return new Collection<PSDriveInfo>
                       {
                           new PSDriveInfo("Git", ProviderInfo, "git:", "Git Provider", null, true)
                       };
        }

        protected override bool IsValidPath(string path)
        {
            throw new NotImplementedException();
        }
    }
}
