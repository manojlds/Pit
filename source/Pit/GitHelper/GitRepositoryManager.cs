using System;
using System.IO;
using LibGit2Sharp;
using Pit.Exceptions;
using cmd;

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

        public static void CreateRepo(string path)
        {
            if (Directory.Exists(path))
            {
                throw new ArgumentException("The path already exists");
            }

            dynamic cmd = new Cmd();
            cmd.git.init(path)();
        }

        public static RepositoryStatus Status(string path)
        {
            if (!Directory.Exists(path) || !Directory.Exists(Path.Combine(path, ".git")))
            {
                throw new ArgumentException("Not a git repo");
            }

            using (var repo = new Repository(path))
            {
                return repo.Index.RetrieveStatus();
            }
        }
    }
}
