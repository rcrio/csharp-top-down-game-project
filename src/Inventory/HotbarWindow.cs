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
            _currentIndex = (_currentIndex + 1) % HotbarSize;
        }
        if (InputManager.ScrollDown())
        {
            _currentIndex = (_currentIndex - 1 + HotbarSize) % HotbarSize;
        }
        // Refactor: should be a better way to do this
        if (InputManager.Hotbar1())
        {
            _currentIndex = 0;
        }
        if (InputManager.Hotbar2())
        {
            _currentIndex = 1;
        }
        if (InputManager.Hotbar3())
        {
            _currentIndex = 2;
        }
        if (InputManager.Hotbar4())
        {
            _currentIndex = 3;
        }
        if (InputManager.Hotbar5())
        {
            _currentIndex = 4;
        }
        if (InputManager.Hotbar6())
        {
            _currentIndex = 5;
        }
        if (InputManager.Hotbar7())
        {
            _currentIndex = 6;
        }
        if (InputManager.Hotbar8())
        {
            _currentIndex = 7;
        }
        if (InputManager.Hotbar9())
        {
            _currentIndex = 8;
        }
        if (InputManager.Hotbar0())
        {
            _currentIndex = 9;
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
            if (i == _currentIndex)
            {
                _slots[i].DrawHightlight();
            }
        }
        
    }
}
