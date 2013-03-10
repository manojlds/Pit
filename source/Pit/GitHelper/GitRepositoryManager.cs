using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            CheckIsValidGitPath(path);

            using (var repo = new Repository(path))
            {
                return repo.Index.RetrieveStatus();
            }
        }

        public static IList<PitCommit> Log(string path, int number)
        {
            CheckIsValidGitPath(path);

            using (var repo = new Repository(path))
            {
                return repo.Commits.Take(number).Select(commit => new PitCommit
                                         {
                                             Sha = commit.Sha,
                                             Message = commit.Message,
                                             Author = commit.Author,
                                             Committer = commit.Committer,
                                         }).ToList();
            }
        }
    }
}
