using System.Numerics;
using Raylib_cs;

public class HotbarWindow : Window
{
    private Inventory _inventory;  // The inventory to display
    private const int HotbarSize = 10; // Number of slots in the hotbar
    private Slot[] _slots;
    private int  _currentIndex;

    public HotbarWindow(Vector2 position, Inventory inventory, InputManager inputManager)
        : base(position, new Vector2(HotbarSize * (Slot.SlotSize + Slot.SlotSpacing) + 20, Slot.SlotSize + 20), inputManager)
    {
        _inventory = inventory;
        _slots = new Slot[HotbarSize];
        _currentIndex = 0;

        float y = 10; // vertical offset in window
        for (int i = 0; i < HotbarSize; i++)
        {
            float x = 10 + i * (Slot.SlotSize + Slot.SlotSpacing); // horizontal position

            var slot = new Slot(new Vector2(x, y), inputManager)
            {
                ItemStack = inventory.GetStack(i)
            };

            _slots[i] = slot;
        }
    }

    public override void Update()
    {
        base.Update();
        foreach (var slot in _slots)
        {
            slot.Update();
        }
        if (InputManager.IsActionPressed(Action.ScrollUp))
        {
            _currentIndex = (_currentIndex + 1) % HotbarSize;
        }
        if (InputManager.IsActionPressed(Action.ScrollDown))
        {
            _currentIndex = (_currentIndex - 1 + HotbarSize) % HotbarSize;
        }
    }

    public override void Draw()
    {
        // Draw background
        Raylib.DrawRectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y, Color.LightGray);
        Raylib.DrawRectangleLines((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y, Color.Black);

        // Draw slots
        for (int i = 0; i < HotbarSize; i++)
        {
            _slots[i].Draw(Position);
            if (i == _currentIndex)
            {
                _slots[i].DrawHightlight();
            }
        }
        
    }
}
