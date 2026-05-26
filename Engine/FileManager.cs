using System.Reflection;

namespace Engine;

public class FileManager
{
    public const string GameFilesFolderName = "GameFiles";
    public DirectoryInfo _directoryInfo = new(GameFilesFolderName);

    public FileManager() { }

    public static string GameFilesPath
    {
        get
        {
            var currentAssembly = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            return Path.Combine(currentAssembly, GameFilesFolderName);
        }
    }

    // Union of Windows and Unix invalid filename chars for consistent cross-platform validation.
    private static readonly char[] InvalidFileNameChars =
        [.. Path.GetInvalidFileNameChars().Union(['*', '?', ':', '<', '>', '|'])];

    public static void CreateFile(string fileName, string[] lines)
    {
        if (fileName.IndexOfAny(InvalidFileNameChars) >= 0)
            throw new ArgumentException($"File name contains invalid characters.", nameof(fileName));

        var createFilePathName = Path.Combine(GameFilesPath, fileName);

        var file = new FileInfo(createFilePathName);
        file.Directory!.Create();
        File.WriteAllLines(file.FullName, lines);
    }

    public static string[] ReadFile(string fileName)
    {
        var readFilePathName = Path.Combine(GameFilesPath, fileName);

        var file = new FileInfo(readFilePathName);
        return File.ReadAllLines(readFilePathName);
    }

    public static void CreateGenerationFile(string fileName, Generation cells)
    {
        CreateFile(fileName, [.. cells.ToCsv()]);
    }

    public static Generation ReadGenerationFile(string fileName)
    {
        var lines = ReadFile(fileName);
        int maxRow = 0, maxCol = 0;
        foreach (var line in lines)
        {
            var tokens = line.Split(',');
            maxRow = Math.Max(maxRow, int.Parse(tokens[0]) + 1);
            maxCol = Math.Max(maxCol, int.Parse(tokens[1]) + 1);
        }
        var generation = new Generation(maxRow, maxCol);
        foreach (var line in lines)
        {
            var tokens = line.Split(',');
            generation[new RowCol(int.Parse(tokens[0]), int.Parse(tokens[1]))] = bool.Parse(tokens[2]);
        }
        return generation;
    }
}
