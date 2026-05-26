using Engine;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class FileManagerTests
{
    [OneTimeTearDown]
    public void CleanupTestGameFilesFolder()
    {
        var directory = new DirectoryInfo(FileManager.GameFilesPath);
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
        var dir = FileManager.GameFilesPath;
        Assert.NotNull(dir);
        Assert.IsTrue(dir.EndsWith(FileManager.GameFilesFolderName));
    }

    [Test]
    public void CanWriteFileWithLinesToGameDirectory()
    {
        const string testFileName = "test.txt";
        var file = new FileManager();
        var fileLines = new string[] { "one", "two" };
        FileManager.CreateFile(testFileName, fileLines);
        var directory = new DirectoryInfo(FileManager.GameFilesPath);
        var foundFiles = directory.EnumerateFiles(testFileName, SearchOption.TopDirectoryOnly);
        Assert.AreEqual(1, foundFiles.Count());
        var foundLines = File.ReadAllLines(foundFiles.First().FullName);
        Assert.AreEqual(fileLines.Length, foundLines.Length);
        Assert.AreEqual(fileLines[0], foundLines[0]);
        Assert.AreEqual(fileLines[1], foundLines[1]);
    }

    [Test]
    public void WriteFileThrowsOnInvalidCharactersFileName()
    {
        const string testFileName = "te*st.txt";
        var file = new FileManager();
        Assert.Throws<ArgumentException>(() => FileManager.CreateFile(testFileName, []));
    }

    [Test]
    public void CanWriteAGenerationToFile()
    {
        const string testFileName = "generationTest.txt";
        var cells = new Generation(1, 2)
        {
            { new RowCol(0, 0), true },
            { new RowCol(0, 1), false }
        };

        var file = new FileManager();
        FileManager.CreateGenerationFile(testFileName, cells);

        var directory = new DirectoryInfo(FileManager.GameFilesPath);
        var foundFiles = directory.EnumerateFiles(testFileName, SearchOption.TopDirectoryOnly);
        var foundLines = File.ReadAllLines(foundFiles.First().FullName);
        Assert.AreEqual(cells.Count, foundLines.Length);

        Assert.AreEqual("0,0,True", foundLines[0]);
        Assert.AreEqual("0,1,False", foundLines[1]);
    }

    [Test]
    public void CanReadFileWithLinesFromGameDirectory()
    {
        const string testFileName = "test.txt";
        var file = new FileManager();
        var fileLines = new string[] { "one", "two" };
        FileManager.CreateFile(testFileName, fileLines);

        var readLines = FileManager.ReadFile(testFileName);
        Assert.AreEqual(fileLines.Length, readLines.Length);
        Assert.AreEqual(fileLines[0], readLines[0]);
        Assert.AreEqual(fileLines[1], readLines[1]);
    }

    [Test]
    public void ReadFileThrowsOnInvalidFile()
    {
        Assert.Throws<FileNotFoundException>(static () => FileManager.ReadFile("notFoundFileName.not"));
    }

    [Test]
    public void CanReadAGenerationFromFile()
    {
        const string testFileName = "generationTest.txt";
        var cell0 = new RowCol(0, 0);
        var cell1 = new RowCol(0, 1);
        var cells = new Generation(1, 2)
        {
            { cell0, true },
            { cell1, false }
        };

        var file = new FileManager();
        FileManager.CreateGenerationFile(testFileName, cells);

        var generation = FileManager.ReadGenerationFile(testFileName);

        Assert.That(generation, Is.InstanceOf(typeof(Generation)));
        Assert.AreEqual(2, generation.Count);
        Assert.IsTrue(generation[cell0]);
        Assert.IsFalse(generation[cell1]);
    }
}
