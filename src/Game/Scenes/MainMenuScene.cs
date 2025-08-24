using System.Numerics;
using Raylib_cs;

public class MainMenuScene : Scene
{
    // InputManager and GameTime inherited from Scene
    private int _optionIndex;
    private int _optionAmount;
    private int _selectorPosY;

    // Constructor for the Main Menu Scene
    public MainMenuScene(InputManager inputManager, GameTime gameTime)
    {
        InputManager = inputManager;
        GameTime = gameTime;
        _optionIndex = 0;
        _optionAmount = 3;
        _selectorPosY = 20;
    }

    public override void Update()
    {
        // Logic for option selector
        if (InputManager.IsActionPressed(Action.MoveUp) || InputManager.IsActionPressed(Action.Up))
        {
            _optionIndex = (_optionIndex - 1 + _optionAmount) % _optionAmount;
        }

        if (InputManager.IsActionPressed(Action.MoveDown) || InputManager.IsActionPressed(Action.Down))
        {
            _optionIndex = (_optionIndex + 1) % _optionAmount;
        }

        // Scene changing or exit
        if (_optionIndex == 0) // Play
        {
            _selectorPosY = 20;
            if (InputManager.IsActionPressed(Action.Select))
            {
                RequestPush = new GameScene(InputManager, GameTime);
            }
        }

        if (_optionIndex == 1) // Options
        {
            _selectorPosY = 50;
            if (InputManager.IsActionPressed(Action.Select))
            {
                RequestPush = new OptionsScene(InputManager, GameTime);
            }
        }

        if (_optionIndex == 2) // Exit
        {
            _selectorPosY = 80;
            if (InputManager.IsActionPressed(Action.Select))
            {
                RequestExit = true;
            }
        }
    }

    public override void Draw()
    {
        Raylib.ClearBackground(Color.Black);

        Font font = FontHandler.GetFontMenu();
        float fontSize = 32;
        float spacing = 1f;
        float lineHeight = fontSize + 10; // smaller padding than +30 for tighter look
        Vector2 startPos = new Vector2(50, 50);

        string[] menuItems = { "Play", "Options", "Exit" };

        for (int i = 0; i < menuItems.Length; i++)
        {
            Vector2 pos = new Vector2(startPos.X, startPos.Y + i * lineHeight);
            Raylib.DrawTextEx(font, menuItems[i], pos, fontSize, spacing, Color.White);

            // Draw selector arrow next to the currently selected option
            if (i == _optionIndex) // assume you track the selected option by index
            {
                Vector2 selectorPos = new Vector2(startPos.X - 30, pos.Y);
                Raylib.DrawTextEx(font, "->", selectorPos, fontSize, spacing, Color.White);
            }
        }
    }

    public override void Unload()
    {

    }
}
