using System.Numerics;
using Raylib_cs;

// slot now is linked to an inventory

public class Slot
{
    public static int SlotSize = 48;
    public static float SlotSpacing = 5;

    public Inventory Inventory { get; private set; }
    public int Index { get; private set; }
    public InputManager InputManager;
    public Vector2 RelativePosition;   // Position relative to window
    public Vector2 WindowPosition;
    public Vector2 DrawPosition;
    public bool IsHovered;

    public Slot(Inventory inventory, int index, Vector2 relativePosition, InputManager inputManager)
    {
        Inventory = inventory;
        Index = index;
        InputManager = inputManager;
        RelativePosition = relativePosition;
    }

    public ItemStack ItemStack
    {
        get => Inventory.ItemStacks[Index];
        set => Inventory.ItemStacks[Index] = value;
    }

    public void Draw(Vector2 windowPosition)
    {
        DrawPosition = windowPosition + RelativePosition;

        // Draw slot background
        Raylib.DrawRectangleRec(new Rectangle(DrawPosition.X, DrawPosition.Y, SlotSize, SlotSize), Color.DarkGray);

        // Draw border inside rectangle bounds to avoid pixel spill
        Raylib.DrawRectangleLinesEx(new Rectangle(DrawPosition.X, DrawPosition.Y, SlotSize, SlotSize), 1, Color.Black);

        // Draw item in slot,
        if (ItemStack != null)
        {   
            // Sprite drawing in slot
            ItemStack.DrawInSlot(DrawPosition, SlotSize);
            DrawStackAmount(ItemStack.Quantity, FontHandler.GetFontNormal(), 16, Color.White);
        }
    }

    // for hotbar
    public void DrawNumber(int number, Font font, int fontSize, Color color)
    {
        // Convert number to text
        string text = number.ToString();

        // Measure text so we can align it properly
        Vector2 textSize = Raylib.MeasureTextEx(font, text, fontSize, 1);

        // Calculate position (top-right, but inset a few pixels for padding)
        Vector2 position = new Vector2(
            DrawPosition.X + SlotSize - textSize.X - 2, // right edge - text width - padding
            DrawPosition.Y + 2 // small padding from top
        );

        // Draw text
        Raylib.DrawTextEx(font, text, position, fontSize, 1, color);
    }

    // Draws a number in the bottom-left corner of the slot
    // Can perhaps refactor, maybe move it to ItemStack? this also means we could move the number amount drawing to stack!
    public void DrawStackAmount(int amount, Font font, int fontSize, Color color)
    {
        if (amount <= 0) return; // Skip drawing if nothing in the stack

        string text = amount.ToString();

        // Measure text so we can align it properly
        Vector2 textSize = Raylib.MeasureTextEx(font, text, fontSize, 1);

        // Calculate position (bottom-left, with padding)
        Vector2 position = new Vector2(
            DrawPosition.X + 2,                          // small padding from left
            DrawPosition.Y + SlotSize - textSize.Y - 2   // small padding from bottom
        );

        // Draw the text
        Raylib.DrawTextEx(font, text, position, fontSize, 1, color);
    }


    public void DrawHightlight()
    {
        Raylib.DrawRectangleLinesEx(new Rectangle(DrawPosition.X, DrawPosition.Y, SlotSize, SlotSize), 3, Color.Gold);
    }


    // Refactor for mouse position
    public void Update()
    {   // Click hitbox for the slot.
        Rectangle rect = new Rectangle(DrawPosition.X, DrawPosition.Y, SlotSize, SlotSize);
        Vector2 mousePos = Raylib.GetMousePosition();
        // Manual "Contains" check
        IsHovered = mousePos.X >= rect.X && mousePos.X <= rect.X + rect.Width &&
                    mousePos.Y >= rect.Y && mousePos.Y <= rect.Y + rect.Height;
    }
}
