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
        Raylib.DrawText("->", 20, _selectorPosY, 20, Color.RayWhite);
        Raylib.DrawText("Play", 50, 20, 20, Color.RayWhite);
        Raylib.DrawText("Options", 50, 50, 20, Color.RayWhite);
        Raylib.DrawText("Exit", 50, 80, 20, Color.RayWhite);
    }

    public override void Unload()
    {

    }
}
