using System.Management.Automation;

namespace Pit
{
    internal class GitDriveInfo : PSDriveInfo
    {
        public GitDriveInfo(string name, ProviderInfo provider, string root, string description, PSCredential credential, bool persist) : base(name, provider, root, description, credential, persist)
        {
        }
    }
}