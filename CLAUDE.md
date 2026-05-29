# Code Style

## Braces
Always enclose control-flow bodies in braces, even when the body is a single statement. This applies to `if`, `else`, `for`, `foreach`, `while`, and `do` blocks.

```csharp
// correct
if (condition)
{
    DoSomething();
}

// incorrect
if (condition)
    DoSomething();
```

## Alignment
Do not use extra spaces to vertically align tokens across adjacent lines.

```csharp
// correct
int up = r == 0 ? rows - 1 : r - 1;
int down = r == rows - 1 ? 0 : r + 1;

// incorrect
int up   = r == 0        ? rows - 1 : r - 1;
int down = r == rows - 1 ? 0        : r + 1;
```

## Declarations
One declaration per line. Do not combine multiple variable declarations or assignments on a single line.

```csharp
// correct
int rows = grid.RowCount;
int cols = grid.ColCount;

// incorrect
int rows = grid.RowCount; int cols = grid.ColCount;
```
