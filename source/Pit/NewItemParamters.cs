using System.Management.Automation;

namespace Pit
{
    public class NewItemParamters
    {
        [Parameter(Mandatory = true)]
        public string RepoPath { get; set; }
    }
}