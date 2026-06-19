using Engine;

namespace TodoCommand;

class Program
{
    static void Main(string[] args)
    {
        // Determine the root path - either from argument or current directory
        var rootPath = args.Length > 0 ? args[0] : ".";

        if (!Directory.Exists(rootPath))
        {
            Console.Error.WriteLine($"Error: Directory '{rootPath}' not found.");
            Environment.Exit(1);
        }

        var finder = new TodoFinder(rootPath);
        var todos = finder.FindTodos();
        finder.PrintTodos(todos);

        // Exit with non-zero code if TODOs were found (useful for CI/CD)
        Environment.Exit(todos.Count > 0 ? 1 : 0);
    }
}
