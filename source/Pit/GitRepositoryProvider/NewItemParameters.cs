using System.Management.Automation;

namespace Pit.GitRepositoryProvider
{
    public class NewItemParameters
    {
        [Parameter(Mandatory = true)]
        public string RepoPath { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter New { get; set; }
    }

}