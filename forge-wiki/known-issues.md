# Known issues

## TODO and FIXME notes

No TODO or FIXME comments were found in the codebase.

## Test status

All tests pass. No failing or skipped tests were found. The test suite covers:
- `GenerationTests.cs`: CSV export, life/extinction detection, next generation resolution, custom rules, array copying, equality, and hashing.
- `GridTests.cs`: Grid creation, cell management, dimension constraints, property change events, size-change events, generation creation (empty, random, from existing).
- `RulesTests.cs`: Default and custom Conway rules (B3/S23), validation, and bitmask generation.
- `GaeaTests.cs`: Simulation control (run, step, pause), delay settings, extinction detection, state transitions, and handler invocation.
- `RowColTests.cs`: Struct creation and property access.
- `FileManagerTests.cs`: File I/O, CSV encoding/decoding, validation, and cleanup.

## Documented limitations

None explicitly documented in code comments.

## Design limitations and observations

- **No redo/undo**: Stepping forward through generations does not allow stepping backward.
- **Grid resizing during simulation**: The UI prevents changing grid dimensions during Run, Step, or Pause states (controls are disabled). When resizing, existing generations are cropped or padded as appropriate (`Grid.CreateGenerationFrom()`).
- **Save/load UI incomplete**: A `GameLoadMenuItem` menu item exists but the corresponding click handler is not fully implemented in the provided code.
- **No custom rule UI**: Rules are hardcoded to Conway's B3/S23; custom rules cannot be selected from the UI (only settable programmatically).
- **Single generation buffer swap**: `Gaea` uses a double-buffer (current/spare) approach rather than triple-buffering, which is sufficient for deterministic turn-based updates.
