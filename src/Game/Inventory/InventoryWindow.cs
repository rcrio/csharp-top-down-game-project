
using System.Numerics;
using System.Runtime.Serialization;
using Raylib_cs;

public class InventoryWindow : Window
{
    private Inventory _inventory;  // The inventory to display
    private Slot[] _slots;         // Array of slot UI elements
    private const int Columns = 10; // Number of columns in the grid
    private ItemStack _draggedItem = null;
    private Slot _currentHoveredSlot = null;
    private TooltipElement _tooltip;

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

        _tooltip = new TooltipElement(Vector2.Zero);
    }

    // Update the window and all its slots, refactor/recomment
    public override void Update()
    {
        base.Update(); // Update base window logic
        _currentHoveredSlot = null;
        Position = new Vector2(
            Raylib.GetScreenWidth() - Size.X - 10,   // 10px from right edge
            Raylib.GetScreenHeight() - Size.Y - 100   // 10px from bottom edge
        );
        for (int i = 0; i < _inventory.Size; i++)
        {
            // Update each slot
            _slots[i].Update();
            // Hover over slot and click logic
            if (_slots[i].IsHovered)
            {
                _currentHoveredSlot = _slots[i];
                var slotStack = _slots[i].ItemStack;

                // ---------------- LEFT CLICK ----------------
                if (InputManager.LeftClick())
                {
                    if (_draggedItem == null)
                    {
                        // Pick up the entire stack from the slot
                        if (slotStack != null)
                        {
                            _draggedItem = slotStack;
                            _slots[i].ItemStack = null;
                            //_draggedFromSlot = _slots[i];
                        }
                    }
                    else
                    {
                        // Place the dragged item into the slot
                        if (slotStack == null)
                        {
                            _slots[i].ItemStack = _draggedItem;
                            _draggedItem = null;
                        }
                        else if (slotStack.GetId() == _draggedItem.GetId())
                        {
                            // Same type: try to merge stacks
                            int total = slotStack.Quantity + _draggedItem.Quantity;
                            int max = _draggedItem.MaxStack;

                            if (total <= max)
                            {
                                slotStack.Add(_draggedItem.Quantity);
                                _draggedItem = null;
                            }
                            else
                            {
                                int remaining = total - max;
                                slotStack.Quantity = max;
                                _draggedItem.Quantity = remaining;
                            }
                        }
                        else
                        {
                            // Different type: swap stacks
                            var temp = slotStack;
                            _slots[i].ItemStack = _draggedItem;
                            _draggedItem = temp;
                        }
                    }
                }

                // ---------------- RIGHT CLICK ----------------

                // Shift right click to get half of a stack of items when cursor is empty
                if (InputManager.SplitStackInHalf() && _draggedItem == null && slotStack != null && slotStack.Quantity > 1)
                {
                    var newStack = _slots[i].ItemStack.Clone(_slots[i].ItemStack.Quantity / 2);
                    _draggedItem = newStack;
                    _slots[i].ItemStack.Remove(_draggedItem.Quantity);
                }

                // Right click logic after shift right click so it doesnt clash
                if (InputManager.RightClick())
                {
                    if (_draggedItem != null)
                    {
                        // Right click with an item in your cursor over an empty slot, to put an item in that slot.
                        if (slotStack == null)
                        {
                            // Place **one item** from cursor into empty slot
                            var newStack = _draggedItem.Clone(1);
                            _slots[i].ItemStack = newStack;
                            _draggedItem.Remove(1);
                        }
                        // Right click with an item in your cursor, over a slot with the same items, to add one item to the slot stack.
                        // If the slot stack is max quantity, this doesn't work.
                        else if (slotStack.GetId() == _draggedItem.GetId() && _draggedItem.Quantity < _draggedItem.MaxStack)
                        {
                            // Add **one item** from cursor into slot
                            _draggedItem.Add(1);
                            _slots[i].ItemStack.Remove(1);
                        }
                    }
                    // Right click with an empty cursor over an item(s) to get one of that item
                    else
                    {
                        var newStack = _slots[i].ItemStack.Clone(1);
                        _draggedItem = newStack;
                        _slots[i].ItemStack.Remove(1);
                    }
                }
                
                // Remove cursor stack if empty
                if (_draggedItem != null && _draggedItem.Quantity <= 0)
                {
                    _draggedItem = null;
                }          
            }
            
        }

        if (_currentHoveredSlot != null && _currentHoveredSlot.ItemStack != null)
        {
            // refactor later on.
            string itemName = _currentHoveredSlot.ItemStack.Item.Name;
            string[] info = [itemName];
            _tooltip.Show(info, Raylib.GetMousePosition());
        }
        else
        {
            _tooltip.Hide();
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
        // Draws the sprite at the mouse when there is an item on mouse.
        // Refactor, 48 is a magic number here
        if (_draggedItem != null)
        {
            Vector2 mousePos = Raylib.GetMousePosition();

            // Draw the item at the mouse
            _draggedItem.DrawInSlot(mousePos, 48);

            // Draw the stack amount at bottom-left of the sprite
            // Can perhaps refactor, maybe move it to ItemStack? this also means we could move the number amount drawing to stack!
            string text = _draggedItem.Quantity.ToString();
            Font font = FontHandler.GetFontNormal();
            int fontSize = 16;

            Vector2 textSize = Raylib.MeasureTextEx(font, text, fontSize, 1);

            Vector2 position = new Vector2(
                mousePos.X + 2,                   // small padding from left
                mousePos.Y + 48 - textSize.Y - 2  // bottom-left inside sprite
            );

            Raylib.DrawTextEx(font, text, position, fontSize, 1, Color.White);
        }

        if (_draggedItem == null)
        {
            _tooltip.Draw();
        }
    }
}