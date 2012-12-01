using System;
using System.IO;
using System.Management.Automation;

namespace Pit
{
    internal class GitDriveInfo : PSDriveInfo
    {
        private const string gitDriveConfigFileName = ".gitdrive";

        public GitDriveInfo(string name, ProviderInfo provider, string root, string description, PSCredential credential, bool persist) : base(name, provider, root, description, credential, persist)
        {
            GitDriveConfigFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                                              gitDriveConfigFileName);
        }

        public string GitDriveConfigFile { get; private set; }
    }
}