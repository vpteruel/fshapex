using System.IO;
using System.Linq;

namespace images.by.clusters
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo[] allFiles = Directory.EnumerateFiles(@"D:\MEGAsync\dataset front", "*.*", SearchOption.AllDirectories)
                .AsParallel()
                .Where(w => w.EndsWith(".png") || w.EndsWith(".jpg") || w.EndsWith(".gif"))
                .Select(s => new FileInfo(s))
                .ToArray();

            for (int k = 4; k <= 7; k++)
            {
                for (int c = 1; c <= k; c++)
                {
                    string source = $@"D:\MEGAsync\dataset front features\k{k}\c{c}";
                    string target = $@"D:\MEGAsync\dataset front by clusters\k{k}\c{c}";

                    FileInfo[] files = Directory.EnumerateFiles(source, "*.*", SearchOption.AllDirectories)
                        .AsParallel()
                        .Where(w => w.EndsWith(".csv"))
                        .Select(s => new FileInfo(s))
                        .ToArray();

                    files.AsParallel().ForAll(f =>
                    {
                        var file = allFiles.AsParallel().Where(w => f.Name.StartsWith(w.Name)).SingleOrDefault();
                        if (file != null)
                            file.CopyTo($@"{target}\{file.Name}", true);
                    });
                }
            }
        }
    }
}
