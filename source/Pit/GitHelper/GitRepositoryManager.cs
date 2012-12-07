using System.IO;
using Pit.Exceptions;

namespace Pit.GitHelper
{
    public static class GitRepositoryManager
    {
        public static void CheckIsValidGitPath(string path)
        {
            if (!Directory.Exists(path) || !Directory.Exists(Path.Combine(path, ".git")))
            {
                throw new NotAGitRepositoryException(path);
            }
        }
    }
}
