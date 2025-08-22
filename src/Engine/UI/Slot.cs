using System.Numerics;
using Raylib_cs;

public class Slot
{
    public static float SlotSize = 48;
    public static float SlotSpacing = 5;

    public InputManager InputManager;
    public Vector2 RelativePosition;   // Position relative to window
    public Vector2 WindowPosition;
    public Vector2 DrawPosition;
    public ItemStack ItemStack;
    public bool IsHovered;

    public Slot(Vector2 relativePosition, InputManager inputManager)
    {
        InputManager = inputManager;
        RelativePosition = relativePosition;
    }
    public void Draw(Vector2 windowPosition)
    {
        DrawPosition = windowPosition + RelativePosition;

        // Draw slot background
        Raylib.DrawRectangleRec(new Rectangle(DrawPosition.X, DrawPosition.Y, SlotSize, SlotSize), Color.DarkGray);

        // Draw border inside rectangle bounds to avoid pixel spill
        Raylib.DrawRectangleLinesEx(new Rectangle(DrawPosition.X, DrawPosition.Y, SlotSize, SlotSize), 1, Color.Black);

        // Draw hover highlight
        if (IsHovered)
        {
            Raylib.DrawRectangleLinesEx(new Rectangle(DrawPosition.X, DrawPosition.Y, SlotSize, SlotSize), 3, Color.Gold);
        }

        // Draw item in slot
        // ItemStack?.Draw(drawPos);
    }


    // Refactor for mouse position
    public void Update()
    {   // Click hitbox for the slot.
        Rectangle rect = new Rectangle(DrawPosition.X, DrawPosition.Y, SlotSize, SlotSize);
        Vector2 mousePos = Raylib.GetMousePosition();
        // Manual "Contains" check
        IsHovered = mousePos.X >= rect.X && mousePos.X <= rect.X + rect.Width &&
                    mousePos.Y >= rect.Y && mousePos.Y <= rect.Y + rect.Height;

        // Click detection
        if (IsHovered && InputManager.IsActionPressed(Action.LeftClick))
        {
            Console.WriteLine("Slot clicked!");
        }
    }
}
