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
    }
}
