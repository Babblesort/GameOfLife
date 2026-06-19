# TODO Finder Slash Command

A convenient way to see all outstanding TODOs in the codebase without leaving the terminal.

## Quick Start

### macOS / Linux

```bash
./todos
```

### Windows

```cmd
todos.bat
```

## Usage

### View all TODOs in the project

```bash
./todos
```

### View TODOs in a specific directory

```bash
./todos ./Engine
./todos ./UI
```

## What It Does

The `todos` slash command:
- Scans the entire codebase for TODO comments
- Searches in `.cs`, `.axaml`, and `.md` files
- Displays results in a formatted table with:
  - File path (relative to project root)
  - Line number
  - TODO text
- Sorts results by file path and line number
- Skips common directories like `.git`, `bin`, `obj`, `.vs`

## Example Output

```
📋 Found 3 TODO(s):

File                    Line  TODO
─────────────────────────────────────────────────────────────
Engine/Generation.cs     42  Optimize cell lookup performance
UI/MainWindow.axaml.cs   156  Add undo/redo functionality
README.md                28  Update documentation
```

## Implementation Details

The slash command is implemented as:

1. **TodoFinder.cs** - Core utility class in the Engine project that:
   - Recursively scans directories
   - Uses regex to find TODO comments
   - Returns structured TodoItem objects

2. **TodoCommand** - New console application that:
   - Uses TodoFinder to scan the codebase
   - Formats and displays results
   - Can be run standalone or via the shell scripts

3. **Shell Scripts**:
   - `todos` (bash) - For macOS/Linux
   - `todos.bat` (batch) - For Windows

## Building

To build the TodoCommand project:

```bash
dotnet build TodoCommand/TodoCommand.csproj
```

Or run it directly:

```bash
dotnet run --project TodoCommand/TodoCommand.csproj
```

## Exit Codes

- `0` - No TODOs found
- `1` - TODOs were found (useful for CI/CD pipelines to fail builds with outstanding TODOs)

## Adding TODOs to Your Code

Simply add a comment with `TODO:` in your code:

```csharp
// TODO: Implement feature X
public void FeatureX()
{
    // ...
}
```

The finder will automatically detect and report it.
