using System.Management.Automation;

namespace Pit
{
    public class NewItemParameters
    {
        [Parameter(Mandatory = true)]
        public string RepoPath { get; set; }
    }
}