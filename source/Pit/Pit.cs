using System.ComponentModel;
using System.Management.Automation;

namespace Pit
{
    [RunInstaller(true)]
    public class Pit : PSSnapIn
    {
        public override string Name
        {
            get { return "Pit"; }
        }

        public override string Vendor
        {
            get { return "StackToHeap"; }
        }

        public override string Description
        {
            get { return "Git provider for Powershell"; }
        }
    }
}
