using System;

namespace Pit.Exceptions
{
    public class RepositoryNotTrackedException : Exception
    {
        public RepositoryNotTrackedException(string repo) : base (GetMessage(repo))
        {
        }

        private static string GetMessage(string repo)
        {
            return string.Format("Repository {0} is not tracked.", repo);
        }
    }
}