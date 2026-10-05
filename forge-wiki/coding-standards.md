# Coding standards

## Language and tooling

- **Language**: C# with .NET 10 SDK (`global.json` pins `"version": "10.0.100"`).
- **C# version**: `preview` language version in project files (`Engine.csproj`, `UI.csproj`, `Tests.csproj`).
- **Platform**: Multi-targeted via Avalonia on desktop (Windows, macOS, Linux).
- **Package manager**: NuGet (via `.csproj` files).

## Formatting and style

Code style rules are documented in `CLAUDE.md`:

### Braces
Always enclose control-flow bodies in braces, even for single-statement bodies (`if`, `else`, `for`, `foreach`, `while`, `do`). See `Generation.cs` and `Grid.cs` for examples of consistently braced blocks.

### Alignment
Do not use extra spaces to vertically align tokens across adjacent lines. See `Grid.cs` for correct alignment practice:
```csharp
int up = r == 0 ? rows - 1 : r - 1;
int down = r == rows - 1 ? 0 : r + 1;
```

### Declarations
One declaration per line; do not combine multiple variable declarations or assignments on a single line. See `Grid.cs`, `Generation.cs`, and `MainWindow.axaml.cs` for consistent declaration practice.

## Nullable reference types and implicit usings

All projects enable nullable reference types (`<Nullable>enable</Nullable>`) and implicit usings (`<ImplicitUsings>enable</ImplicitUsings>`) in their project files. This improves safety and reduces boilerplate.

## Testing

- **Framework**: NUnit 4 with `NUnit3TestAdapter` and `Microsoft.NET.Test.Sdk`.
- **Command**: `dotnet test` (runs all test projects).
- **Test locations**: `Tests/` directory with files named `*Tests.cs` (e.g., `GenerationTests.cs`, `GridTests.cs`).
- **Coverage**: Tests cover all Engine components (`Grid`, `Generation`, `Rules`, `Gaea`, `RowCol`, `FileManager`). The UI is not tested directly (no test project for UI).
- **Status**: All tests pass; no skipped or failing tests found.

## Naming conventions

- **Namespaces**: Match project names (`Engine`, `UI`, `Tests`).
- **Classes**: PascalCase (`Generation`, `Gaea`, `VisualSettings`, `MainWindow`).
- **Events and delegates**: PascalCase (`PropertyChanged`, `GridCellClicked`, `Stopped`, `GenerationUpdateHandler`).
- **Methods and properties**: PascalCase (`CreateEmptyGeneration`, `ResolveNextGeneration`, `DelayMilliseconds`).
- **Private fields**: camelCase prefixed with `_` (e.g., `_cells`, `_gaea`, `_tokenSource` in `MainWindow.axaml.cs`).
- **Local variables**: camelCase (e.g., `rows`, `cols`, `generation`, `nextGen` in tests).
- **Constants**: PascalCase (e.g., `MinRows`, `MaxRows`, `DefaultRows`, `DefaultDelayMilliseconds` in `Grid.cs` and `Gaea.cs`).

## Project structure

```
GameOfLife/
├── Engine/
│   ├── Engine.csproj
│   ├── Grid.cs
│   ├── Generation.cs
│   ├── Rules.cs
│   ├── Gaea.cs
│   ├── FileManager.cs
│   ├── RowCol.cs
│   └── CellClickedEventArgs.cs
├── UI/
│   ├── UI.csproj
│   ├── Program.cs
│   ├── App.axaml.cs
│   ├── MainWindow.axaml.cs
│   ├── GamePanel.cs
│   ├── VisualSettings.cs
│   ├── SettingsWindow.axaml.cs
│   └── GameFiles/
│       └── Pulsar.txt
└── Tests/
    ├── Tests.csproj
    ├── GenerationTests.cs
    ├── GridTests.cs
    ├── RulesTests.cs
    ├── GaeaTests.cs
    ├── RowColTests.cs
    └── FileManagerTests.cs
```

## Key design patterns

- **INotifyPropertyChanged**: Used in `Grid` and bindings in Avalonia XAML for reactive UI updates.
- **Event-driven communication**: `Grid` fires size-change events; `GamePanel` fires cell-click events; `Gaea` fires stopped events.
- **Async/await**: `Gaea` uses `Task`, `Task.Delay()`, and `CancellationToken` for non-blocking simulation.
- **Record structs**: `RowCol` uses `readonly record struct` for value semantics and equality.
- **Bitmask optimization**: `Rules` uses integer bitmasks for O(1) neighbor-count lookups instead of hash sets or linear searches.

## Dependencies

See `Engine.csproj`, `UI.csproj`, and `Tests.csproj` for package versions:
- Avalonia 12.0.4 and related packages (Desktop, Fluent theme, Inter fonts, ColorPicker)
- NUnit 4.* with NUnit3TestAdapter 6.* and Microsoft.NET.Test.Sdk 18.*
