// Generic Window class
using System.Numerics;
using Raylib_cs;

public class Window : UIElement
{
    public InputManager InputManager { get; private set; }
    public List<UIElement> Children { get; private set; } = new List<UIElement>();
    public Color BackgroundColor { get; private set; } = Color.DarkGray;

    public Window(Vector2 position, Vector2 size, InputManager inputManager) : base(position, size)
    {
        InputManager = inputManager;
    }

    public void AddChild(UIElement child)
    {
        Children.Add(child);
    }

    public override void Update()
    {
        // Update each child relative to window position
        foreach (var child in Children)
        {
            Vector2 originalPos = child.Position;

            // Offset child position by window position
            child.Position += Position;

            child.Update();

            // Restore local position after update
            child.Position = originalPos;
        }
    }

    public override void Draw()
    {
        // Draw the window background
        Raylib.DrawRectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y, BackgroundColor);

        foreach (var child in Children)
        {
            // This is the local position inside the window.
            Vector2 originalPos = child.Position;

            // We calculate the actual position, by adding the window position to offset it.
            child.Position += Position; 
            // Now, we draw the children and it will show up in the window.
            child.Draw();
            // This reassignment is needed as the draw is happening very fast, literally a frame.
            // We need to reassign its local position again, so it can continue to calculate
            // according to the window position. If we didn't, it'd just drift away as we don't
            // reassign the local position.
            child.Position = originalPos;
        }
    }
}