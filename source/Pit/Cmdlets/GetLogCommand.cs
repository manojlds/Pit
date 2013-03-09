using System.Management.Automation;
using Pit.Exceptions;
using Pit.GitHelper;

namespace Pit.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GitLog")]
    public class GetLogCommand : PitCmdlet
    {
        [Parameter(Mandatory = false, Position = 1)]
        public int Number { get; set; }

        protected override void ProcessRecord()
        {
            var trackedRepository = GitConfigManager.GetTrackedRepository(Name);
            if (trackedRepository == null) throw new RepositoryNotTrackedException(Name);

            WriteObject(GitRepositoryManager.Log(trackedRepository.Path, Number));
        }
    }
}