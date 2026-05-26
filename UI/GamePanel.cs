using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Engine;
using EngineGrid = Engine.Grid;

namespace UI;

public class GamePanel : Control
{
    private EngineGrid? _grid;
    private Generation? _cells;
    private bool[] _snapshot = Array.Empty<bool>();
    private int _snapshotRows;
    private int _snapshotCols;
    private VisualSettings _settings = new();

    public event EventHandler<CellClickedEventArgs>? GridCellClicked;

    private int RowCount => _grid?.RowCount ?? 1;
    private int ColCount => _grid?.ColCount ?? 1;
    private double CellHeight => (Bounds.Height - 5) / RowCount;
    private double CellWidth => (Bounds.Width - 5) / ColCount;

    public EngineGrid? Grid
    {
        get => _grid;
        set
        {
            if (_grid != null)
                _grid.PropertyChanged -= OnGridPropertyChanged;
            _grid = value;
            if (_grid != null)
                _grid.PropertyChanged += OnGridPropertyChanged;
            InvalidateVisual();
        }
    }

    public Generation? Cells
    {
        get => _cells;
        set
        {
            _cells = value;
            if (value != null)
            {
                if (_snapshot.Length != value.Count)
                {
                    _snapshot = new bool[value.Count];
                }
                _snapshotRows = value.Rows;
                _snapshotCols = value.Cols;
                value.CopyTo(_snapshot);
            }
            InvalidateVisual();
        }
    }

    public VisualSettings Settings
    {
        get => _settings;
        set { _settings = value; InvalidateVisual(); }
    }

    private void OnGridPropertyChanged(object? sender, PropertyChangedEventArgs e) =>
        InvalidateVisual();

    public override void Render(DrawingContext context)
    {
        if (_grid == null) return;

        var gridLinePen = new Pen(new SolidColorBrush(_settings.GridLineColor), _settings.GridLineThickness);
        var cellBrush   = new SolidColorBrush(_settings.CellColor);

        var gridWidth  = ColCount * CellWidth;
        var gridHeight = RowCount * CellHeight;

        for (int r = 0; r <= RowCount; r++)
            context.DrawLine(gridLinePen, new Point(0, r * CellHeight), new Point(gridWidth, r * CellHeight));

        for (int c = 0; c <= ColCount; c++)
            context.DrawLine(gridLinePen, new Point(c * CellWidth, 0), new Point(c * CellWidth, gridHeight));

        if (_snapshotRows > 0 && _snapshotCols > 0)
        {
            for (int r = 0; r < _snapshotRows; r++)
            {
                for (int c = 0; c < _snapshotCols; c++)
                {
                    if (_snapshot[r * _snapshotCols + c])
                    {
                        context.DrawRectangle(cellBrush, null,
                            new Rect(c * CellWidth, r * CellHeight, CellWidth, CellHeight));
                    }
                }
            }
        }
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        var pos = e.GetPosition(this);
        var col = (int)(pos.X / CellWidth);
        var row = (int)(pos.Y / CellHeight);
        GridCellClicked?.Invoke(this, new CellClickedEventArgs { Cell = new RowCol(row, col) });
    }
}
