using Avalonia.Media;

namespace UI;

public class VisualSettings
{
    public static readonly double[] Thicknesses = { 0.5, 1.0, 2.0 };

    public Color BorderColor        { get; set; } = Colors.Gray;
    public int   BorderThicknessIndex  { get; set; } = 1;   // Medium
    public Color GridLineColor      { get; set; } = Colors.LightGray;
    public int   GridLineThicknessIndex { get; set; } = 1;  // Medium
    public Color CellColor          { get; set; } = Color.FromArgb(180, 34, 139, 34);

    public double BorderThickness   => Thicknesses[BorderThicknessIndex];
    public double GridLineThickness => Thicknesses[GridLineThicknessIndex];
}
