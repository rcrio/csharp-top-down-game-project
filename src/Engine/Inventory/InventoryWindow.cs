
using System.Numerics;
using Raylib_cs;

public class InventoryWindow : Window
{
    private Inventory _inventory;  // The inventory to display
    private Slot[] _slots;         // Array of slot UI elements
    private const int Columns = 10; // Number of columns in the grid

    // Constructor: creates a new inventory window at the specified position
    public InventoryWindow(Vector2 position, Inventory inventory, InputManager inputManager) : base(position, new Vector2(800, 800), inputManager) // Initial window size
    {
        _inventory = inventory;
        _slots = new Slot[inventory.Size];

        // Calculate how many rows are needed for this inventory
        int rows = inventory.Size / Columns;
        if (inventory.Size % Columns != 0) rows += 1; // If there are remainders, we need another row.

        // Calculate required height based on number of rows, slot size, spacing, and padding
        float requiredHeight = 10 + rows * (Slot.SlotSize + Slot.SlotSpacing) + 10;

        //Ensure window is tall enough to fit all slots
        Size = new Vector2(Size.X, Math.Max(Size.Y, requiredHeight));
        Console.WriteLine(Size);

        // Create and position each slot in absolute screen coordinates
        for (int i = 0; i < inventory.Size; i++)
        {
            int row = i / Columns;
            int col = i % Columns;

            float x = 10 + col * (Slot.SlotSize + Slot.SlotSpacing); // Relative
            float y = 10 + row * (Slot.SlotSize + Slot.SlotSpacing);

            var slot = new Slot(new Vector2(x, y), inputManager)
            {
                ItemStack = inventory.GetStack(i)
            };

            _slots[i] = slot;
        }
    
    }

    // Update the window and all its slots
    public override void Update()
    {
        base.Update(); // Update base window logic
        Position = new Vector2(
                Raylib.GetScreenWidth() - Size.X - 10, // 10px padding from right
                10 // 10px padding from top
            );
        foreach (var slot in _slots)
        {
            // Update each slot
            slot.Update();
            if (slot.IsHovered && InputManager.IsActionPressed(Action.LeftClick))
            {
                Console.WriteLine("Slot clicked!");
            }
        }
    }

    // Draw the window and all slots
    public override void Draw()
    {
        // Draw window background
        Raylib.DrawRectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y, Color.LightGray);
        Raylib.DrawRectangleLines((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y, Color.Black);

        // Draw slots relative to current window position
        foreach (var slot in _slots)
        {
            slot.Draw(Position);
            
            if (slot.IsHovered)
            {
                slot.DrawHightlight();
            }
        }
            
    }
}