using Avalonia.Controls;
using Avalonia.Interactivity;

namespace UI;

public partial class SettingsWindow : Window
{
    private readonly VisualSettings _settings;
    private readonly Action _onChanged;
    private bool _loading = true;

    public SettingsWindow(VisualSettings settings, Action onChanged)
    {
        _settings = settings;
        _onChanged = onChanged;
        InitializeComponent();
        LoadSettings();
        SubscribeToChanges();
        _loading = false;
    }

    private void LoadSettings()
    {
        BorderThicknessCombo.SelectedIndex   = _settings.BorderThicknessIndex;
        GridLineThicknessCombo.SelectedIndex = _settings.GridLineThicknessIndex;
        BorderColorView.Color   = _settings.BorderColor;
        GridLineColorView.Color = _settings.GridLineColor;
        CellColorView.Color     = _settings.CellColor;
    }

    private void SubscribeToChanges()
    {
        BorderThicknessCombo.SelectionChanged   += (_, _) => Apply();
        GridLineThicknessCombo.SelectionChanged += (_, _) => Apply();
        BorderColorView.ColorChanged   += (_, _) => Apply();
        GridLineColorView.ColorChanged += (_, _) => Apply();
        CellColorView.ColorChanged     += (_, _) => Apply();
    }

    private void Apply()
    {
        if (_loading) return;
        if (BorderThicknessCombo.SelectedIndex >= 0)
            _settings.BorderThicknessIndex = BorderThicknessCombo.SelectedIndex;
        if (GridLineThicknessCombo.SelectedIndex >= 0)
            _settings.GridLineThicknessIndex = GridLineThicknessCombo.SelectedIndex;
        _settings.BorderColor   = BorderColorView.Color;
        _settings.GridLineColor = GridLineColorView.Color;
        _settings.CellColor     = CellColorView.Color;
        _onChanged();
    }

    private void CloseHandler(object? sender, RoutedEventArgs e) => Close();
}
