using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace count.dataset
{
    class Program
    {
        static void Main(string[] args)
        {
            var path1 = @"D:\MEGAsync\dataset front";
            var path2 = @"D:\MEGAsync\dataset front features";

            var dataset = new Dictionary<string, string>();
            var clusters = new List<Tuple<string, string, string, string>>();

            foreach (var dir in new DirectoryInfo(path1).GetDirectories())
                foreach (var file in dir.GetFiles())
                    dataset.Add(file.Name, dir.Name);

            foreach (var dirK in new DirectoryInfo(path2).GetDirectories())
            {
                if (!dirK.Name.In("k4", "k5", "k6", "k7"))
                    continue;

                foreach (var dirC in dirK.GetDirectories())
                {
                    if (!dirC.Name.In("c1", "c2", "c3", "c4", "c5", "c6", "c7"))
                        continue;

                    foreach (var file in dirC.GetFiles())
                    {
                        var filename = file.Name.Replace(".csv", "");

                        if (!dataset.ContainsKey(filename))
                            continue;

                        clusters.Add(Tuple.Create(dirK.Name, dirC.Name, dataset[filename], filename));
                    }
                }
            }

            var counters = new List<Tuple<string, string, int>>();

            foreach (var cluster in clusters)
            {
                var key = cluster.Item1 + cluster.Item2;
                if (counters.Any(f => f.Item1 == key && f.Item2 == cluster.Item3))
                {
                    var index = counters.FindIndex(f => f.Item1 == key && f.Item2 == cluster.Item3);
                    var count = counters[index].Item3;
                    counters[index] = Tuple.Create(key, cluster.Item3, ++count);
                }
                else
                    counters.Add(Tuple.Create(key, cluster.Item3, 1));
            }

            foreach (var counter in counters)
                Console.WriteLine($"{counter.Item1};{counter.Item3};{counter.Item2}");

            Console.ReadKey();
        }
    }
    public static class Extension
    {
        public static bool In<T>(this T self, params T[] args)
        {
            if (self == null || args == null || args.Length == 0)
                return false;
            foreach (var arg in args)
                if (self.Equals(arg))
                    return true;
            return false;
        }
    }
}
