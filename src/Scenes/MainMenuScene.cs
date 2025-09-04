using System.Numerics;
using Raylib_cs;

public class MainMenuScene : Scene
{
    // Inherited:
    // InputManager InputManager
    // GameTime GameTime
    // MusicManager MusicManager
    private int _optionIndex;
    private int _optionAmount;
    private Texture2D _titleScreen;
    string[] menuItems = { "New Game", "Load Game", "Multiplayer", "Stats", "Options", "Credits", "Exit" };
    private Font _font;


    // Constructor for the Main Menu Scene
    public MainMenuScene(InputManager inputManager, GameTime gameTime) : base(inputManager, gameTime)
    {
        _optionIndex = 0;
        _optionAmount = menuItems.Length;
    }

    public override void Update()
    {
        // Update delta time
        GameTime.Update();

        // Logic for option selector
        if (InputManager.MoveUpSelect() || InputManager.ArrowUp())
        {
            _optionIndex = (_optionIndex - 1 + _optionAmount) % _optionAmount;
        }

        if (InputManager.MoveDownSelect() || InputManager.ArrowDown())
        {
            _optionIndex = (_optionIndex + 1) % _optionAmount;
        }

        // Scene changing or exit
        if (_optionIndex == 0) // New Game
        {
            if (InputManager.Select())
            {
                RequestPush = new GameScene(InputManager, GameTime);
            }
        }

        if (_optionIndex == 1) // Load Game
        {
            if (InputManager.Select())
            {
            }
        }

        if (_optionIndex == 2) // Multiplayer
        {
            if (InputManager.Select())
            {
            }
        }

        if (_optionIndex == 3) // Stats
        {
            if (InputManager.Select())
            {
            }
        }

        if (_optionIndex == 4) // Options
        {
            if (InputManager.Select())
            {
            }
        }

        if (_optionIndex == 5) // Credits
        {
            if (InputManager.Select())
            {
            }
        }

        if (_optionIndex == 6) // Exit
        {
            if (InputManager.Select())
            {
                RequestExit = true;
            }
        }
    }

    public override void Draw()
    {
        Raylib.ClearBackground(Color.Black);

        // Draw the title
        int titleX = (Raylib.GetScreenWidth() - _titleScreen.Width) / 2;
        Raylib.DrawTexture(_titleScreen, titleX, 20, Color.White);
        
        float fontSize = 48;
        float lineSpacing = fontSize + 10;

        // Start from bottom
        float startY = Raylib.GetScreenHeight() - 100; // 50 px padding from bottom
        Vector2 startPos = new Vector2(50, startY);

        for (int i = 0; i < menuItems.Length; i++)
        {
            // Draw from bottom up
            int reverseIndex = menuItems.Length - 1 - i;
            Vector2 pos = new Vector2(startPos.X, startPos.Y - i * lineSpacing);

            // Draw menu item
            Raylib.DrawTextEx(_font, menuItems[reverseIndex], pos, fontSize, 1f, Color.White);

            // Draw selector arrow if selected
            if (reverseIndex == _optionIndex)
            {
                Vector2 selectorPos = new Vector2(startPos.X - 30, pos.Y);
                Raylib.DrawTextEx(_font, "*", selectorPos, fontSize, 1f, Color.White);
            }
        }
    }
    public override void Load()
    {
        _titleScreen = AssetManager.LoadTexture("Textures/title_smaller.png", 1000, 500);
        _font = FontHandler.GetFontMenu(); // Don't need to call unload, font handler does this in the main game class
        Music = AssetManager.LoadMusic("Sound/Music/song_longview1_temp.mp3");
    }
    public override void Unload()
    {
        AssetManager.UnloadTexture(_titleScreen);
        AssetManager.UnloadMusic(Music);
        _titleScreen = new Texture2D();
        Music = new Music();
    }
}
