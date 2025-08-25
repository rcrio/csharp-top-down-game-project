using System.Numerics;

// Base UIElement class
public abstract class UIElement : UIElementStatic
{
    public UIElement(Vector2 position, Vector2 size) : base(position, size)
    {
        Position = position;
        Size = size;
    }

    public abstract void Update();
}