// Inventory window example
using System.Numerics;

public class InventoryWindow : Window
{
    public InventoryWindow(Vector2 position) : base(position, new Vector2(200, 200))
    {
        // Add slots with positions relative to the window's top-left corner
        AddChild(new Slot(new Vector2(10, 10), new Vector2(16, 16)));
        AddChild(new Slot(new Vector2(40, 10), new Vector2(16, 16)));
    }
}