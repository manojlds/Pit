using System;
using System.Management.Automation;

namespace Pit
{
    internal class GitDriveInfo : PSDriveInfo
    {
        public string GitDriveConfigFile { get; private set; }

        public GitDriveInfo(string name, ProviderInfo provider, string root, string description, PSCredential credential, bool persist) : base(name, provider, root, description, credential, persist)
        {
            GitDriveConfigFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }
    }
}