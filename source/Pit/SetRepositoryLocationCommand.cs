using System.Management.Automation;
using Pit.Exceptions;
using Pit.GitDriveConfig;

namespace Pit
{
    [Cmdlet(VerbsCommon.Set, "RepoLocation")]
    public class SetRepositoryLocationCommand : PSCmdlet
    {
        private readonly IGitConfigManager gitConfigManager;

        public SetRepositoryLocationCommand()
        {
            gitConfigManager = new GitConfigManager();
        }

        [Parameter(Mandatory = true)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            var trackedRepository = gitConfigManager.GetTrackedRepository(Name);
            if (trackedRepository == null) throw new RepositoryNotTrackedException(Name);

            SessionState.Path.SetLocation(trackedRepository.Path);
        }
    }
}