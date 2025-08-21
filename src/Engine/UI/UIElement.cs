using System.Numerics;

// Base UIElement class
public abstract class UIElement
{
    public Vector2 Position { get; set; } // Local position (relative to parent)
    public Vector2 Size { get; set; }

    public UIElement(Vector2 position, Vector2 size)
    {
        Position = position;
        Size = size;
    }

    public abstract void Update();
    public abstract void Draw();
}