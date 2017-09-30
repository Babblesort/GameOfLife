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
        [OneTimeTearDown]
        public void CleanupTestGameFilesFolder()
        {
            var directory = new DirectoryInfo(new FileManager().GameFilesPath);
            foreach (var testFile in directory.GetFiles())
            {
                testFile.Delete();
            }
            directory.Delete();
        }

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
        public void GameFilesPathProperty()
        {
            var dir = new FileManager().GameFilesPath;
            Assert.NotNull(dir);
            Assert.IsTrue(dir.EndsWith(FileManager.GameFilesFolderName));
        }

        [Test]
        public void CanWriteFileWithLinesToGameDirectory()
        {
            const string testFileName = "test.txt";
            var file = new FileManager();
            var fileLines = new string[] { "one", "two" };
            file.CreateFile(testFileName, fileLines);
            var directory = new DirectoryInfo(file.GameFilesPath);
            var foundFiles = directory.EnumerateFiles(testFileName, SearchOption.TopDirectoryOnly);
            Assert.AreEqual(1, foundFiles.Count());
            var foundLines = File.ReadAllLines(foundFiles.First().FullName);
            Assert.AreEqual(fileLines.Length, foundLines.Length);
        }

        [Test]
        public void WriteFileThrowsOnInvalidCharactersFileName()
        {
            const string testFileName = "te*st.txt";
            var file = new FileManager();
            Assert.Throws<ArgumentException>(() => file.CreateFile(testFileName, new string[0]));
        }

        [Test]
        public void CanWriteAGenerationToFile()
        {
            const string testFileName = "generationTest.txt";
            var cells = new Generation
            {
                { new RowCol(0, 0), true },
                { new RowCol(0, 1), false }
            };

            var file = new FileManager();
            file.CreateGenerationFile(testFileName, cells);

            var directory = new DirectoryInfo(file.GameFilesPath);
            var foundFiles = directory.EnumerateFiles(testFileName, SearchOption.TopDirectoryOnly);
            var foundLines = File.ReadAllLines(foundFiles.First().FullName);
            Assert.AreEqual(cells.Count, foundLines.Length);

            Assert.AreEqual("0,0,True", foundLines[0]);
            Assert.AreEqual("0,1,False", foundLines[1]);
        }

    }
}
