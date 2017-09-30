using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class FileManager
    {
        public static string GameFilesFolderName = "GameFiles";
        public DirectoryInfo _directoryInfo = new DirectoryInfo(GameFilesFolderName);

        public FileManager() { }

        public string GameFilesPath
        {
            get
            {
                var currentAssembly = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                return Path.Combine(currentAssembly, GameFilesFolderName);
            }
        }

        public void CreateFile(string fileName, string[] lines)
        {
            var createFilePathName = Path.Combine(GameFilesPath, fileName);

            var file = new FileInfo(createFilePathName);
            file.Directory.Create();
            File.WriteAllLines(file.FullName, lines);
        }

        public string[] ReadFile(string fileName)
        {
            var readFilePathName = Path.Combine(GameFilesPath, fileName);

            var file = new FileInfo(readFilePathName);
            return File.ReadAllLines(readFilePathName);
        }

        public void CreateGenerationFile(string fileName, Generation cells)
        {
            CreateFile(fileName, cells.ToCsv().ToArray());
        }

        public Generation ReadGenerationFile(string fileName)
        {
            var lines = ReadFile(fileName);
            var generation = new Generation();
            foreach (var line in lines)
            {
                var tokens = line.Split(',');
                generation.Add(new RowCol(int.Parse(tokens[0]), int.Parse(tokens[1])), bool.Parse(tokens[2]));
            }
            return generation;
        }
    }
}
