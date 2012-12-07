using System;

namespace Pit.Exceptions
{
    public class NotAGitRepositoryException : Exception
    {
        public NotAGitRepositoryException(string path)
            : base(GetMessage(path))
        {
        }

        private static string GetMessage(string path)
        {
            return string.Format("{0} is not a git repository", path);
        }
    }
}