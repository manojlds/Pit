﻿using System.Management.Automation;
using Pit.Exceptions;
using Pit.GitHelper;

namespace Pit.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GitStatus")]
    public class GetStatusCommand : PitCmdlet
    {
        protected override void ProcessRecord()
        {
            var trackedRepository = GitConfigManager.GetTrackedRepository(Name);
            if (trackedRepository == null) throw new RepositoryNotTrackedException(Name);

            WriteObject(GitRepositoryManager.Status(trackedRepository.Path));
        }
    }
}
