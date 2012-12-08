using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation.Provider;

namespace Pit.GitRepositoryProvider
{
    public class ReadmeContentReader : IContentReader
    {
        private int currentOffset;
        private readonly string[] lines;

        public ReadmeContentReader(string path)
        {
            this.lines = File.ReadAllLines(Path.Combine(path, "README.md"));
        }

        public void Dispose()
        {
        }

        public IList Read(long readCount)
        {
            if (currentOffset < 0 || currentOffset >= lines.Count())
            {
                return null;
            }

            var rowsRead = 0;
            var results = new List<string>();
            while (rowsRead < readCount && currentOffset < lines.Count())
            {
                results.Add(lines[currentOffset]);
                rowsRead++;
                currentOffset++;
            }
            return results;
        }

        public void Seek(long offset, SeekOrigin origin)
        {
            throw new System.NotImplementedException();
        }

        public void Close()
        {
            Dispose();
        }
    }
}