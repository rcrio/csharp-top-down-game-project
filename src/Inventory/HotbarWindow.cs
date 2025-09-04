using System.Numerics;
using Raylib_cs;

public class HotbarWindow : Window
{
    private Inventory _inventory;  // The inventory to display
    private const int HotbarSize = 10; // Number of slots in the hotbar
    private Slot[] _slots;

    public HotbarWindow(Vector2 position, Inventory inventory, InputManager inputManager)
        : base(position, new Vector2(HotbarSize * (Slot.SlotSize + Slot.SlotSpacing) + 20, Slot.SlotSize + 20), inputManager)
    {
        _inventory = inventory;
        _slots = new Slot[HotbarSize];

        for (int i = 0; i < HotbarSize; i++)
        {
            float x = 10 + i * (Slot.SlotSize + Slot.SlotSpacing);
            float y = 10;
            _slots[i] = new Slot(_inventory, i, new Vector2(x, y), InputManager);
        }
    }

    public override void Update()
    {
        base.Update();
        // this repositions window according to resolution
        Position = new Vector2(
            (Raylib.GetScreenWidth() - Size.X) / 2,        // center horizontally
            Raylib.GetScreenHeight() - Size.Y - 10        // 10px padding from bottom
        );
        for (int i = 0; i < HotbarSize; i++)
        {
            // Update each slot
            _slots[i].Update();
        }

        // Hotbar scrolling check
        if (InputManager.ScrollUp())
        {
            _inventory.currentSelectedIndex = (_inventory.currentSelectedIndex + 1) % HotbarSize;
        }
        if (InputManager.ScrollDown())
        {
            _inventory.currentSelectedIndex = (_inventory.currentSelectedIndex - 1 + HotbarSize) % HotbarSize;
        }
        // Refactor: should be a better way to do this
        if (InputManager.Hotbar1())
        {
            _inventory.currentSelectedIndex = 0;
        }
        if (InputManager.Hotbar2())
        {
            _inventory.currentSelectedIndex = 1;
        }
        if (InputManager.Hotbar3())
        {
            _inventory.currentSelectedIndex = 2;
        }
        if (InputManager.Hotbar4())
        {
            _inventory.currentSelectedIndex = 3;
        }
        if (InputManager.Hotbar5())
        {
            _inventory.currentSelectedIndex = 4;
        }
        if (InputManager.Hotbar6())
        {
            _inventory.currentSelectedIndex = 5;
        }
        if (InputManager.Hotbar7())
        {
            _inventory.currentSelectedIndex = 6;
        }
        if (InputManager.Hotbar8())
        {
            _inventory.currentSelectedIndex = 7;
        }
        if (InputManager.Hotbar9())
        {
            _inventory.currentSelectedIndex = 8;
        }
        if (InputManager.Hotbar0())
        {
            _inventory.currentSelectedIndex = 9;
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
            int number = (i + 1) % HotbarSize;
            _slots[i].DrawNumber(number, FontHandler.GetFontNormal(), 16, Color.White);
            if (i == _inventory.currentSelectedIndex)
            {
                _slots[i].DrawHightlight();
            }
        }
        
    }
}
