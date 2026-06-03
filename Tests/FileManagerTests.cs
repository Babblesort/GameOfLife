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
        Assert.That(new FileManager(), Is.Not.Null);
    }

    [Test]
    public void ExposesGameFileDirectoryProperty()
    {
        Assert.That(FileManager.GameFilesFolderName, Is.EqualTo("GameFiles"));
    }

    [Test]
    public void GameFilesPathProperty()
    {
        var dir = FileManager.GameFilesPath;
        Assert.That(dir, Is.Not.Null);
        Assert.That(dir, Does.EndWith(FileManager.GameFilesFolderName));
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
        Assert.That(foundFiles.Count(), Is.EqualTo(1));
        var foundLines = File.ReadAllLines(foundFiles.First().FullName);
        Assert.That(foundLines.Length, Is.EqualTo(fileLines.Length));
        Assert.That(foundLines[0], Is.EqualTo(fileLines[0]));
        Assert.That(foundLines[1], Is.EqualTo(fileLines[1]));
    }

    [Test]
    public void WriteFileThrowsOnInvalidCharactersFileName()
    {
        const string testFileName = "te*st.txt";
        var file = new FileManager();
        Assert.That((Action)(() => FileManager.CreateFile(testFileName, [])), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void CanWriteAGenerationToFile()
    {
        const string testFileName = "generationTest.txt";
        var cells = new Generation(1, 2);
        cells[new RowCol(0, 0)] = true;
        cells[new RowCol(0, 1)] = false;

        var file = new FileManager();
        FileManager.CreateGenerationFile(testFileName, cells);

        var directory = new DirectoryInfo(FileManager.GameFilesPath);
        var foundFiles = directory.EnumerateFiles(testFileName, SearchOption.TopDirectoryOnly);
        var foundLines = File.ReadAllLines(foundFiles.First().FullName);
        Assert.That(foundLines.Length, Is.EqualTo(cells.Count));

        Assert.That(foundLines[0], Is.EqualTo("0,0,True"));
        Assert.That(foundLines[1], Is.EqualTo("0,1,False"));
    }

    [Test]
    public void CanReadFileWithLinesFromGameDirectory()
    {
        const string testFileName = "test.txt";
        var file = new FileManager();
        var fileLines = new string[] { "one", "two" };
        FileManager.CreateFile(testFileName, fileLines);

        var readLines = FileManager.ReadFile(testFileName);
        Assert.That(readLines.Length, Is.EqualTo(fileLines.Length));
        Assert.That(readLines[0], Is.EqualTo(fileLines[0]));
        Assert.That(readLines[1], Is.EqualTo(fileLines[1]));
    }

    [Test]
    public void ReadFileThrowsOnInvalidFile()
    {
        Assert.That((Action)(static () => FileManager.ReadFile("notFoundFileName.not")), Throws.TypeOf<FileNotFoundException>());
    }

    [Test]
    public void CanReadAGenerationFromFile()
    {
        const string testFileName = "generationTest.txt";
        var cell0 = new RowCol(0, 0);
        var cell1 = new RowCol(0, 1);
        var cells = new Generation(1, 2);
        cells[cell0] = true;
        cells[cell1] = false;

        var file = new FileManager();
        FileManager.CreateGenerationFile(testFileName, cells);

        var generation = FileManager.ReadGenerationFile(testFileName);

        Assert.That(generation, Is.InstanceOf(typeof(Generation)));
        Assert.That(generation.Count, Is.EqualTo(2));
        Assert.That(generation[cell0], Is.True);
        Assert.That(generation[cell1], Is.False);
    }
}
