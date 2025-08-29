using System.Numerics;

// Base UIElement class
public abstract class UIElementStatic
{
    public Vector2 Position { get; set; } // Local position (relative to parent)
    public Vector2 Size { get; set; }

    public UIElementStatic(Vector2 position, Vector2 size)
    {
        Position = position;
        Size = size;
    }

    public abstract void Draw();
}