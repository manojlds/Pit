using System.Management.Automation;
using Pit.Exceptions;
using Pit.GitDriveConfig;
using Pit.GitHelper;

namespace Pit.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GitStatus")]
    public class GetStatusCommand : PSCmdlet
    {
        private readonly IGitConfigManager gitConfigManager;

        [Parameter(Mandatory = true, Position = 0)]
        public string Name { get; set; }

        public GetStatusCommand()
        {
            gitConfigManager = new GitConfigManager();
        }

        protected override void ProcessRecord()
        {
            var trackedRepository = gitConfigManager.GetTrackedRepository(Name);
            if (trackedRepository == null) throw new RepositoryNotTrackedException(Name);

            WriteObject(GitRepositoryManager.Status(trackedRepository.Path));
        }
    }
}
