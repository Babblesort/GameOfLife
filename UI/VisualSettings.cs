using Avalonia.Media;

namespace UI;

public class VisualSettings
{
    public static readonly double[] Thicknesses = [0.5, 1.0, 2.0];

    public Color BorderColor { get; set; } = Color.FromArgb(0xFF, 0x33, 0x32, 0x30);
    public int BorderThicknessIndex { get; set; } = 2;   // Thick
    public Color GridLineColor { get; set; } = Color.FromArgb(0x61, 0xFA, 0xF0, 0xE6);
    public int GridLineThicknessIndex { get; set; } = 0;  // Thin
    public Color CellColor { get; set; } = Color.FromArgb(0xFF, 0x00, 0x63, 0xB1);
    public double BorderThickness => Thicknesses[BorderThicknessIndex];
    public double GridLineThickness => Thicknesses[GridLineThicknessIndex];
}
