using System.Numerics;
using Raylib_cs;

public class TooltipElement : UIElementStatic
{
    public string[] Lines { get; private set; } = new string[0]; // store multiple lines
    public bool Visible { get; private set; } = false;
    private int FontSize = 16;

    public TooltipElement(Vector2 position) : base(position, Vector2.Zero) { }

    // Show the tooltip with multiple lines at the mouse position
    public void Show(string[] lines, Vector2 mousePosition)
    {
        Lines = lines;
        Position = mousePosition;
        Visible = true;
    }

    // Hide the tooltip
    public void Hide()
    {
        Visible = false;
    }

    public override void Draw()
    {
        if (!Visible || Lines.Length == 0) return;

        Font font = FontHandler.GetFontNormal();
        float spacing = 1;
        float lineHeight = FontSize + 4;
        int padding = 6;

        // Measure max line width
        float textWidth = 0;
        foreach (var line in Lines)
        {
            float lineWidth = Raylib.MeasureTextEx(font, line, FontSize, spacing).X;
            if (lineWidth > textWidth) textWidth = lineWidth;
        }

        float textHeight = Lines.Length * lineHeight;

        // Draw position offset from mouse
        Vector2 drawPos = Position + new Vector2(16, 16);

        // Clamp to screen
        int screenWidth = Raylib.GetScreenWidth();
        int screenHeight = Raylib.GetScreenHeight();

        if (drawPos.X + textWidth + padding * 2 > screenWidth)
            drawPos.X = screenWidth - textWidth - padding * 2;
        if (drawPos.Y + textHeight + padding * 2 > screenHeight)
            drawPos.Y = screenHeight - textHeight - padding * 2;

        // Draw background
        Raylib.DrawRectangle((int)(drawPos.X - padding), (int)(drawPos.Y - padding),
                             (int)(textWidth + padding * 2), (int)(textHeight + padding * 2),
                             Color.DarkGray);
        Raylib.DrawRectangleLines((int)(drawPos.X - padding), (int)(drawPos.Y - padding),
                                  (int)(textWidth + padding * 2), (int)(textHeight + padding * 2),
                                  Color.Black);

        // Draw each line
        for (int i = 0; i < Lines.Length; i++)
        {
            Vector2 linePos = new Vector2(drawPos.X, drawPos.Y + i * lineHeight);
            Raylib.DrawTextEx(font, Lines[i], linePos, FontSize, spacing, Color.White);
        }
    }
}
