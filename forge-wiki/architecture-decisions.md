# Architecture decisions

## Components and communication

The application is divided into three projects:

**Engine** (platform-independent game logic)
- `Grid`: Manages grid dimensions (1–200 rows and columns) and provides cell coordinate utilities. Implements `INotifyPropertyChanged` for property change notifications; fires `GridSizeIncreased` and `GridSizeDecreased` events when dimensions change.
- `Generation`: Stores cell state in a compact boolean array (row-major order); computes the next generation by applying rules to all cells. Implements `IEquatable<Generation>` and `IHashable`. Provides `ToCsv()` and `CopyTo()` methods for serialization and in-place copying.
- `Rules`: Encodes Conway's B3/S23 rules (birth on 3 neighbors, survive on 2–3 neighbors) using a bitmask for O(1) lookup. Accepts custom rule sets via constructor.
- `Gaea`: Drives the asynchronous simulation loop (named after the Greek primordial earth goddess). Manages `Run()`, `Step()`, and `Pause()` modes; handles delay configuration (25–500 ms) and fires a `Stopped` event on extinction.
- `FileManager`: Handles saving and loading generations to/from CSV files in a `GameFiles` folder; validates filenames against combined Windows/Unix invalid character sets for cross-platform consistency.
- `RowCol`: A readonly record struct representing a grid coordinate.
- `CellClickedEventArgs`: Carries cell click events from the UI to the logic layer.

**UI** (Avalonia 12 desktop application)
- `MainWindow`: Central orchestrator; manages game state (Idle, Run, Step, Pause), initializes and controls `Gaea`, handles grid resizing, keyboard shortcuts (platform-specific modifiers), and menu/button events.
- `GamePanel`: Custom Avalonia `Control` that renders the grid with `DrawingContext`; draws grid lines and live cells as rectangles. Implements mouse-click detection to report toggled cells.
- `VisualSettings`: Configuration for cell color, grid line color, border color, and thickness; used by `GamePanel` and `MainWindow.axaml`.
- `SettingsWindow`: Exposes color pickers (via Avalonia ColorPicker controls) and thickness combo boxes for customizing visual appearance.
- `App`: Avalonia application lifecycle manager.
- `Program`: Entry point; configures and starts the Avalonia app.

**Tests** (NUnit 4 test suite for Engine)
- `GenerationTests`, `GridTests`, `RulesTests`, `GaeaTests`, `RowColTests`, `FileManagerTests`: Unit tests covering all Engine components.

## Data flow

1. User clicks cells in `GamePanel` → fires `CellClickedEventArgs` → `MainWindow` toggles `PregameCells` (a `Generation`) and updates the display.
2. User presses Run/Step → `MainWindow` creates/updates `Gaea` with current grid and cells → `Gaea` spawns an async task to compute next generations.
3. `Generation.ResolveNextGeneration()` applies `Rules` to compute the next state; `Gaea` buffers swap current and spare generations.
4. `Gaea` invokes the `UpdateVisualization` callback (supplied by `MainWindow`) → `MainWindow` updates the UI on the dispatcher thread → `GamePanel.Render()` draws the grid.

## Toroidal topology

Both `Grid` (neighbor lookup) and `Generation` (in `ResolveNextGeneration`) implement wraparound: cells at the edges treat the opposite edge as a neighbor. This is computed inline during simulation for efficiency (see `Generation.cs` and `Grid.cs` neighbor methods).

## Data persistence

Generations are saved to CSV format (row, column, alive/dead) in a `GameFiles` folder. `FileManager` reads these files back and reconstructs `Generation` objects. The UI does not currently expose save/load UI buttons (only a `GameLoad` menu item is referenced but not fully implemented in the provided code).

## Hosting and deployment

- **Platform**: .NET 10 SDK (cross-platform) with Avalonia 12 for desktop UI (Windows, macOS, Linux).
- **Build**: `dotnet run --project UI` or `dotnet build`.
- **Distribution**: Executable built from the solution; no containerization, cloud deployment, or external services are involved.

## Notable libraries and rationale

- **Avalonia 12**: Cross-platform UI framework; chosen for native desktop experience on Windows, macOS, and Linux without platform-specific code.
- **.NET 10**: Latest .NET runtime; provides nullable reference types, implicit usings, and modern language features (C# 13 preview).
- **NUnit 4**: Testing framework; lightweight and integration with `dotnet test`.
- **Avalonia.Controls.ColorPicker**: Built-in color picker widget for settings UI.
- **Avalonia.Themes.Fluent**: Fluent design theme for consistent Avalonia appearance.

## Performance considerations

- **O(1) rule lookup**: Rules uses a bitmask to avoid linear search of neighbor counts.
- **Compact storage**: `Generation` uses a flat `bool[]` (not object arrays) for memory efficiency.
- **Row-major indexing**: Grid cells indexed as `r * Cols + c` for cache-friendly access.
- **Async simulation**: `Gaea` runs generation computation on a background thread to avoid blocking the UI; uses `Task.Delay()` for configurable sleep between steps.
