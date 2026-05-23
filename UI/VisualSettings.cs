using Avalonia.Media;

namespace UI;

public class VisualSettings
{
    public static readonly double[] Thicknesses = { 0.5, 1.0, 2.0 };

    public Color BorderColor           { get; set; } = Colors.Linen;
    public int   BorderThicknessIndex  { get; set; } = 2;   // Thick
    public Color GridLineColor         { get; set; } = Colors.Linen;
    public int   GridLineThicknessIndex { get; set; } = 0;  // Thin
    public Color CellColor             { get; set; } = Colors.SteelBlue;

    public double BorderThickness   => Thicknesses[BorderThicknessIndex];
    public double GridLineThickness => Thicknesses[GridLineThicknessIndex];
}
