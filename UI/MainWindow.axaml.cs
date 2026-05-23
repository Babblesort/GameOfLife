using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using Engine;
using EngineGrid = Engine.Grid;

namespace UI;

public partial class MainWindow : Window
{
    private EngineGrid _grid = null!;
    private Gaea? _gaea;
    private readonly VisualSettings _visualSettings = new();
    private SettingsWindow? _settingsWindow;

    public enum GameStates { Idle, Run, Step, Pause }

    public Generation PregameCells { get; private set; } = null!;
    public GameStates GameState { get; private set; } = GameStates.Idle;

    public MainWindow()
    {
        InitializeComponent();
        Opened += OnOpened;
    }

    private void OnOpened(object? sender, EventArgs e)
    {
        _grid = new EngineGrid();
        gamePanel.Grid = _grid;
        PregameCells = _grid.CreateEmptyGeneration();

        _grid.GridSizeIncreased += OnGridSizeIncreased;
        _grid.GridSizeDecreased += OnGridSizeDecreased;
        gamePanel.GridCellClicked += OnGridCellClicked;

        SpeedSlider.Minimum = Gaea.MinDelayMilliseconds;
        SpeedSlider.Maximum = Gaea.MaxDelayMilliseconds;
        SpeedSlider.TickFrequency = 100;
        SpeedSlider.SmallChange = 50;
        SpeedSlider.Value = Gaea.MaxDelayMilliseconds + Gaea.MinDelayMilliseconds - Gaea.DefaultDelayMilliseconds;

        TrackRows.Minimum = EngineGrid.MinRows;
        TrackRows.Maximum = EngineGrid.MaxRows;
        TrackRows.TickFrequency = 25;
        TrackRows.SmallChange = 10;
        UpDownRows.Minimum = EngineGrid.MinRows;
        UpDownRows.Maximum = EngineGrid.MaxRows;

        TrackCols.Minimum = EngineGrid.MinCols;
        TrackCols.Maximum = EngineGrid.MaxCols;
        TrackCols.TickFrequency = 25;
        TrackCols.SmallChange = 10;
        UpDownCols.Minimum = EngineGrid.MinCols;
        UpDownCols.Maximum = EngineGrid.MaxCols;

        TrackRows.Value = EngineGrid.DefaultRows;
        TrackCols.Value = EngineGrid.DefaultCols;
        UpDownRows.Value = EngineGrid.DefaultRows;
        UpDownCols.Value = EngineGrid.DefaultCols;

        SpeedSlider.PropertyChanged += (_, args) =>
        {
            if (args.Property == Slider.ValueProperty && _gaea != null)
                _gaea.DelayMilliseconds = (int)(SpeedSlider.Maximum + SpeedSlider.Minimum - SpeedSlider.Value);
        };

        TrackRows.PropertyChanged += (_, args) =>
        {
            if (args.Property == Slider.ValueProperty)
                SynchronizeRowsValue((int)TrackRows.Value);
        };

        TrackCols.PropertyChanged += (_, args) =>
        {
            if (args.Property == Slider.ValueProperty)
                SynchronizeColsValue((int)TrackCols.Value);
        };

        UpDownRows.ValueChanged += (_, _) =>
        {
            if (UpDownRows.Value.HasValue)
                SynchronizeRowsValue((int)UpDownRows.Value.Value);
        };

        UpDownCols.ValueChanged += (_, _) =>
        {
            if (UpDownCols.Value.HasValue)
                SynchronizeColsValue((int)UpDownCols.Value.Value);
        };

        CheckboxLockRowAndCols.IsCheckedChanged += (_, _) =>
        {
            if (CheckboxLockRowAndCols.IsChecked != true) return;
            var lower = Math.Min((int)TrackRows.Value, (int)TrackCols.Value);
            TrackRows.Value = lower;
            TrackCols.Value = lower;
        };

        ApplyVisualSettings();
        SetupKeyboardShortcuts();
        SetUiForGameState(GameStates.Idle);
        UpdateGameVisualization(0, PregameCells);
    }

    private void OnGridSizeIncreased(object? sender, EventArgs e)
    {
        _grid.Cells.Except(PregameCells.Keys).ToList().ForEach(k => PregameCells.Add(k, false));
        UpdateGameVisualization(0, PregameCells);
    }

    private void OnGridSizeDecreased(object? sender, EventArgs e)
    {
        PregameCells.Keys.Except(_grid.Cells).ToList().ForEach(k => PregameCells.Remove(k));
        UpdateGameVisualization(0, PregameCells);
    }

    private void OnGridCellClicked(object? sender, CellClickedEventArgs e)
    {
        if (GameState != GameStates.Idle) return;
        PregameCells[e.Cell] = !PregameCells[e.Cell];
        UpdateGameVisualization(0, PregameCells);
    }

    private void RaiseGaeaOnDemand()
    {
        if (_gaea == null)
        {
            var generationZero = PregameCells.HasLiveCells ? PregameCells : _grid.CreateRandomGeneration();
            _gaea = new Gaea(_grid, new Rules(), UpdateGameVisualization, generationZero);
        }
        _gaea.DelayMilliseconds = (int)(SpeedSlider.Maximum + SpeedSlider.Minimum - SpeedSlider.Value);
    }

    private void VisualizationSettingsHandler(object? sender, RoutedEventArgs e)
    {
        if (_settingsWindow is { IsVisible: true })
        {
            _settingsWindow.Activate();
            return;
        }
        _settingsWindow = new SettingsWindow(_visualSettings, ApplyVisualSettings);
        _settingsWindow.Show(this);
    }

    private void ApplyVisualSettings()
    {
        gamePanelBorder.BorderBrush     = new SolidColorBrush(_visualSettings.BorderColor);
        gamePanelBorder.BorderThickness = new Thickness(_visualSettings.BorderThickness);
        gamePanel.Settings = _visualSettings;
    }

    private void RunGameHandler(object? sender, RoutedEventArgs e) => GameRun();
    private void StepGameHandler(object? sender, RoutedEventArgs e) => GameStep();
    private void PauseGameHandler(object? sender, RoutedEventArgs e) => GamePause();
    private void NewGameHandler(object? sender, RoutedEventArgs e) => GameNew();
    private void ExitGameHandler(object? sender, RoutedEventArgs e) => Environment.Exit(0);

    private void GameNew()
    {
        SetUiForGameState(GameStates.Idle);
        PregameCells = _grid.CreateEmptyGeneration();
        _gaea?.Clear();
        _gaea = null;
        UpdateGameVisualization(0, PregameCells);
    }

    private void GameRun()
    {
        SetUiForGameState(GameStates.Run);
        RaiseGaeaOnDemand();
        _gaea!.Run();
    }

    private void GameStep()
    {
        SetUiForGameState(GameStates.Step);
        RaiseGaeaOnDemand();
        _gaea!.Step();
    }

    private void GamePause()
    {
        SetUiForGameState(GameStates.Pause);
        RaiseGaeaOnDemand();
        _gaea!.Pause();
    }

    private void UpdateGameVisualization(int generationNumber, Generation cells)
    {
        Dispatcher.UIThread.Post(() =>
        {
            lblGeneration.Text = generationNumber.ToString("N0");
            gamePanel.Cells = cells;
        });
    }

    private void SynchronizeRowsValue(int value)
    {
        if (CheckboxLockRowAndCols.IsChecked == true)
        {
            TrackCols.Value = value;
            UpDownCols.Value = value;
        }
        UpDownRows.Value = value;
        TrackRows.Value = value;
        _grid.RowCount = value;
    }

    private void SynchronizeColsValue(int value)
    {
        if (CheckboxLockRowAndCols.IsChecked == true)
        {
            TrackRows.Value = value;
            UpDownRows.Value = value;
        }
        UpDownCols.Value = value;
        TrackCols.Value = value;
        _grid.ColCount = value;
    }

    private void SetupKeyboardShortcuts()
    {
        var mod = OperatingSystem.IsMacOS() ? KeyModifiers.Meta : KeyModifiers.Control;
        ApplyShortcut(GameNewMenuItem,       Key.N,        mod);
        ApplyShortcut(GameRunMenuItem,       Key.R,        mod);
        ApplyShortcut(GameStepMenuItem,      Key.T,        mod);
        ApplyShortcut(GamePauseMenuItem,     Key.P,        mod);
        ApplyShortcut(GameExitMenuItem,      Key.Q,        mod);
        ApplyShortcut(VisualizationMenuItem, Key.OemComma, mod);
    }

    private static void ApplyShortcut(MenuItem item, Key key, KeyModifiers modifiers)
    {
        var gesture = new KeyGesture(key, modifiers);
        item.HotKey = gesture;
        item.InputGesture = gesture;
    }

    private void ToggleControlsHandler(object? sender, RoutedEventArgs e)
    {
        controlsPanel.IsVisible = !controlsPanel.IsVisible;
        btnToggleControls.Content = controlsPanel.IsVisible ? "◀" : "▶";
    }

    private void SetUiForGameState(GameStates state)
    {
        GameState = state;
        var isIdle = state == GameStates.Idle;
        var isRunning = state == GameStates.Run;
        var isPausedOrStepped = state is GameStates.Pause or GameStates.Step;

        GameLoadMenuItem.IsEnabled = true;
        GameNewMenuItem.IsEnabled = true;
        GameRunMenuItem.IsEnabled = !isRunning;
        GameStepMenuItem.IsEnabled = true;
        GamePauseMenuItem.IsEnabled = isRunning;

        btnRun.IsEnabled = !isRunning;
        btnStep.IsEnabled = true;
        btnPause.IsEnabled = isRunning;
        btnNew.IsEnabled = true;

        SpeedSlider.IsEnabled = true;
        var lockDims = isIdle;
        CheckboxLockRowAndCols.IsEnabled = lockDims;
        TrackRows.IsEnabled = lockDims;
        TrackCols.IsEnabled = lockDims;
        UpDownRows.IsEnabled = lockDims;
        UpDownCols.IsEnabled = lockDims;
    }
}
