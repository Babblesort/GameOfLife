# Game of Life

A desktop implementation of [Conway's Game of Life](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life) built with .NET 10 and Avalonia 12.

## Features

- Customizable grid up to 200 × 200 cells
- Click cells to toggle them before starting
- Run, Step, and Pause controls with configurable simulation speed
- Random starting generation when Run is pressed on an empty grid
- Toroidal topology — cells wrap at the edges
- Customizable colors and border/grid line thickness
- Generation counter and ms/gen performance display
- Keyboard shortcuts for all game controls

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Build and run

```bash
git clone <repo-url>
cd GameOfLife
dotnet run --project UI
```

## Keyboard shortcuts

| Action | macOS | Windows / Linux |
|---|---|---|
| New Game | Cmd+N | Ctrl+N |
| Run | Cmd+R | Ctrl+R |
| Step | Cmd+T | Ctrl+T |
| Pause | Cmd+P | Ctrl+P |
| Visualization Settings | Cmd+, | Ctrl+, |
| Exit | Cmd+Q | Ctrl+Q |

## Project structure

```
GameOfLife/
├── Engine/     # Platform-independent game logic
├── UI/         # Avalonia desktop application
└── Tests/      # NUnit test suite for the Engine
```

**Engine** contains the core simulation: `Grid` manages dimensions and cell coordinates; `Generation` stores the cell state and computes the next generation; `Rules` encodes Conway's B3/S23 rules using a bitmask for O(1) lookup; `Gaea` drives the async simulation loop; `FileManager` handles saving and loading generations.

**UI** is an Avalonia 12 desktop app using the Fluent theme. `GamePanel` is a custom `Control` that renders the grid with `DrawingContext`. `SettingsWindow` exposes color pickers for cells, grid lines, and border.

## Running the tests

```bash
dotnet test
```
