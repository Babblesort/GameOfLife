# Game of Life

A desktop implementation of [Conway's Game of Life](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life) built with .NET 10 and Avalonia 12. The application provides an interactive cellular automaton simulator with a customizable grid up to 200 × 200 cells, click-to-toggle cell editing, playback controls with adjustable simulation speed, and customizable visual appearance.

## Key features

- Interactive grid with cell toggling (before simulation starts)
- Run, Step, and Pause controls with configurable simulation speed (25–500 ms/generation)
- Random starting generation when Run is pressed on an empty grid
- Toroidal topology — cells wrap at the edges
- Customizable colors and grid/border line thickness via a dedicated settings window
- Generation counter and performance display (ms/gen)
- Keyboard shortcuts for macOS (Cmd) and Windows/Linux (Ctrl)
- Generation save/load functionality

## Documentation

- [Coding standards](coding-standards.md): languages, tooling, formatting, testing, naming, and project structure
- [Architecture decisions](architecture-decisions.md): components, communication, data stores, hosting, and notable libraries
- [Known issues](known-issues.md): TODO and FIXME notes, test status, and documented limits
