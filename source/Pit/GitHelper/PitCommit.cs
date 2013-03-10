using LibGit2Sharp;

namespace Pit.GitHelper
{
    public class PitCommit
    {
        public string Sha { get; set; }

        public string Message { get; set; }

        public Signature Author { get; set; }

        public Signature Committer { get; set; }
    }
}