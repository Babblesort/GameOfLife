# Architecture Decisions

## Stack
- **.NET 10** / **C#** with nullable reference types and implicit usings enabled.
- **Avalonia 12** (Fluent theme) for the cross-platform desktop UI.
- **NUnit** test suite covering the `Engine` project only.

## Project layout
```
GameOfLife/
├── Engine/   # Platform-independent game logic (Grid, Generation, Rules, Gaea, FileManager)
├── UI/       # Avalonia desktop app (MainWindow, GamePanel, SettingsWindow)
└── Tests/    # NUnit tests for Engine
```

## UI bootstrapping
`Program.cs` → `App.axaml.cs` (`OnFrameworkInitializationCompleted`) → `MainWindow`.  
`MainWindow.OnOpened` wires up all control bindings, event handlers, and keyboard shortcuts; no MVVM framework is used — everything is code-behind.

## Window inventory
| Class | File | Purpose |
|---|---|---|
| `MainWindow` | `MainWindow.axaml[.cs]` | Primary game window (1300×800) |
| `SettingsWindow` | `SettingsWindow.axaml[.cs]` | Visualization settings (color pickers, thickness combos) |

## Patterns
- Custom `Control` (`GamePanel`) renders the grid via `DrawingContext`.
- `Gaea` drives the async simulation loop; UI is updated via `Dispatcher.UIThread.Post`.
- Keyboard shortcuts assigned at runtime in `SetupKeyboardShortcuts` using `KeyGesture`.
- No splash screen exists yet; `App.OnFrameworkInitializationCompleted` sets `desktop.MainWindow = new MainWindow()` directly.
