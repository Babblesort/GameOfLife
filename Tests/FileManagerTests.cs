using Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class FileManagerTests
    {
        [Test]
        public void CanBeCreated()
        {
            Assert.NotNull(new FileManager());
        }

        [Test]
        public void ExposesGameFileDirectoryProperty()
        {
            Assert.AreEqual(FileManager.GameFilesFolderName, "GameFiles");
        }

        [Test]
        public void GameFilesDirProperty()
        {
            var dir = new FileManager().GameFilesPath;
            Assert.NotNull(dir);
            Assert.IsTrue(dir.EndsWith(FileManager.GameFilesFolderName));
        }

        [Test]
        public void CanWriteFileWithLinesToGameDirectory()
        {
            var file = new FileManager();
            var fileLines = new string[] { "one", "two" };
            file.CreateFile("test.txt", fileLines);
            var directory = new DirectoryInfo(file.GameFilesPath);
            var foundFiles = directory.EnumerateFiles("test.txt", SearchOption.TopDirectoryOnly);
            Assert.AreEqual(1, foundFiles.Count());
            var foundLines = File.ReadAllLines(foundFiles.First().FullName);
            Assert.AreEqual(fileLines.Length, foundLines.Length);
        }
    }
}
