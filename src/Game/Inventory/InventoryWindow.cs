
using System.Numerics;
using Raylib_cs;

public class InventoryWindow : Window
{
    private Inventory _inventory;  // The inventory to display
    private Slot[] _slots;         // Array of slot UI elements
    private const int Columns = 10; // Number of columns in the grid
    private ItemStack _draggedItem = null;
    private Slot _draggedFromSlot = null;

    // Constructor: creates a new inventory window at the specified position
    public InventoryWindow(Vector2 position, Inventory inventory, InputManager inputManager) : base(position, new Vector2(565, 235), inputManager) // Initial window size
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

        // Create and position each slot in absolute screen coordinates
        for (int i = 0; i < inventory.Size; i++)
        {
            int row = i / Columns;
            int col = i % Columns;
            float x = 10 + col * (Slot.SlotSize + Slot.SlotSpacing);
            float y = 10 + row * (Slot.SlotSize + Slot.SlotSpacing);

            _slots[i] = new Slot(inventory, i, new Vector2(x, y), InputManager);
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
        for (int i = 0; i < _inventory.Size; i++)
        {
            // Update each slot
            _slots[i].Update();
            // Hover over slot and click logic
            if (_slots[i].IsHovered)
            {
                if (InputManager.IsActionPressed(Action.LeftClick))
                {
                    // If the slot is not empty, we take the item from slot
                    var tempItem = _slots[i].ItemStack;
                    _slots[i].ItemStack = _draggedItem;
                    _draggedItem = tempItem;
                    _draggedFromSlot = _slots[i];
                }
                if (_draggedItem == null)
                {
                    // update logic for drawing information window
                }
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
        for (int i = 0; i < _inventory.Size; i++)
        {
            _slots[i].Draw(Position);
            // ehhhhh change 10 to hotbar size or some shit (refactor)
            int number = (i + 1) % 10;
            if (i >= 0 && i < 10)
            {
                _slots[i].DrawNumber(number, FontHandler.GetFontNormal(), 16, Color.White);
                if (_slots[i].IsHovered)
                {
                    _slots[i].DrawHightlight();
                }
            }
        }

        
        
        // Refactor, 48 is a magic number here
        if (_draggedItem != null)
        {
            _draggedItem.DrawInSlot(Raylib.GetMousePosition(), 48);
        }
            
    }
}