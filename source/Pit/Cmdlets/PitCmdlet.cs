using System.Management.Automation;
using Pit.GitDriveConfig;

namespace Pit.Cmdlets
{
    public class PitCmdlet : PSCmdlet
    {
        internal readonly IGitConfigManager GitConfigManager;

        [Parameter(Mandatory = true, Position = 0)]
        public string Name { get; set; }

        protected PitCmdlet()
        {
            GitConfigManager = new GitConfigManager();
        }
    }
}