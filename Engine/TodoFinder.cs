using System.Text.RegularExpressions;

namespace Engine;

/// <summary>
/// Scans the codebase for TODO comments and reports them.
/// </summary>
public class TodoFinder
{
    private readonly string _rootPath;
    private readonly string[] _fileExtensions = { ".cs", ".axaml", ".md" };
    private readonly string[] _excludeDirs = { ".git", "bin", "obj", ".vs", "node_modules" };

    public TodoFinder(string rootPath = ".")
    {
        _rootPath = Path.GetFullPath(rootPath);
    }

    /// <summary>
    /// Finds all TODO comments in the codebase.
    /// </summary>
    public List<TodoItem> FindTodos()
    {
        var todos = new List<TodoItem>();

        try
        {
            ScanDirectory(_rootPath, todos);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error scanning directory: {ex.Message}");
        }

        return todos.OrderBy(t => t.FilePath).ThenBy(t => t.LineNumber).ToList();
    }

    private void ScanDirectory(string dirPath, List<TodoItem> todos)
    {
        try
        {
            var files = Directory.GetFiles(dirPath);
            foreach (var file in files)
            {
                if (_fileExtensions.Any(ext => file.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
                {
                    ScanFile(file, todos);
                }
            }

            var subdirs = Directory.GetDirectories(dirPath);
            foreach (var subdir in subdirs)
            {
                var dirName = Path.GetFileName(subdir);
                if (!_excludeDirs.Contains(dirName, StringComparer.OrdinalIgnoreCase))
                {
                    ScanDirectory(subdir, todos);
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            // Skip directories we can't access
        }
    }

    private void ScanFile(string filePath, List<TodoItem> todos)
    {
        try
        {
            var lines = File.ReadAllLines(filePath);
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var match = Regex.Match(line, @"//\s*TODO\s*:?\s*(.+?)(?:\s*$|//)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var todoText = match.Groups[1].Value.Trim();
                    var relativePath = Path.GetRelativePath(_rootPath, filePath);
                    todos.Add(new TodoItem
                    {
                        FilePath = relativePath,
                        LineNumber = i + 1,
                        Text = todoText
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading file {filePath}: {ex.Message}");
        }
    }

    /// <summary>
    /// Prints all TODOs in a formatted table.
    /// </summary>
    public void PrintTodos(List<TodoItem> todos)
    {
        if (todos.Count == 0)
        {
            Console.WriteLine("✓ No TODOs found!");
            return;
        }

        Console.WriteLine($"\n📋 Found {todos.Count} TODO(s):\n");

        // Calculate column widths
        var maxFileWidth = Math.Max("File".Length, todos.Max(t => t.FilePath.Length));
        var maxLineWidth = Math.Max("Line".Length, todos.Max(t => t.LineNumber.ToString().Length));
        var maxTextWidth = Math.Max("TODO".Length, todos.Max(t => t.Text.Length));

        // Print header
        Console.WriteLine($"{"File",-maxFileWidth}  {"Line",maxLineWidth}  {"TODO",-maxTextWidth}");
        Console.WriteLine(new string('-', maxFileWidth + maxLineWidth + maxTextWidth + 6));

        // Print rows
        foreach (var todo in todos)
        {
            Console.WriteLine($"{todo.FilePath,-maxFileWidth}  {todo.LineNumber,maxLineWidth}  {todo.Text,-maxTextWidth}");
        }

        Console.WriteLine();
    }
}

/// <summary>
/// Represents a single TODO item found in the codebase.
/// </summary>
public class TodoItem
{
    public string FilePath { get; set; } = string.Empty;
    public int LineNumber { get; set; }
    public string Text { get; set; } = string.Empty;
}
