using System.Management.Automation;
using Pit.Exceptions;
using Pit.GitDriveConfig;

namespace Pit.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "RepoLocation")]
    public class SetRepositoryLocationCommand : PitCmdlet
    {
        protected override void ProcessRecord()
        {
            var trackedRepository = GitConfigManager.GetTrackedRepository(Name);
            if (trackedRepository == null) throw new RepositoryNotTrackedException(Name);

            SessionState.Path.SetLocation(trackedRepository.Path);
        }
    }
}