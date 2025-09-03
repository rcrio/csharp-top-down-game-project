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
    private SoundPool _selectSoundPool;
    private SoundPool _confirmSoundPool;
    string[] menuItems = { "New Game", "Load Game", "Multiplayer", "Stats", "Options", "Credits", "Exit" };
    string [] itemsLoaded = { "title.png" };
    

    // Constructor for the Main Menu Scene
    public MainMenuScene(InputManager inputManager, GameTime gameTime, MusicManager musicManager) : base(inputManager, gameTime, musicManager)
    {
        _optionIndex = 0;
        _optionAmount = menuItems.Length;
        _selectSoundPool = new SoundPool("select.mp3", 8);
        _confirmSoundPool = new SoundPool("confirm.mp3", 8);
        Load();
    }

    public override void Update()
    {
        // Update delta time
        GameTime.Update();

        // Update music
        MusicManager.Play("song_gravity.mp3", 7f); // fade in 1 second        
        MusicManager.Update(GameTime.DeltaTime);

        // Logic for option selector
        if (InputManager.MoveUpSelect() || InputManager.ArrowUp())
        {
            _optionIndex = (_optionIndex - 1 + _optionAmount) % _optionAmount;
            _selectSoundPool.Play();
        }

        if (InputManager.MoveDownSelect() || InputManager.ArrowDown())
        {
            _optionIndex = (_optionIndex + 1) % _optionAmount;
            _selectSoundPool.Play();
        }

        // Scene changing or exit
        if (_optionIndex == 0) // New Game
        {
            if (InputManager.Select())
            {
                _confirmSoundPool.Play();
                RequestPush = new GameScene(InputManager, GameTime, MusicManager);
            }
        }

        if (_optionIndex == 1) // Load Game
        {
            if (InputManager.Select())
            {
                _confirmSoundPool.Play();
            }
        }

        if (_optionIndex == 2) // Multiplayer
        {
            if (InputManager.Select())
            {
                _confirmSoundPool.Play();
            }
        }

        if (_optionIndex == 3) // Stats
        {
            if (InputManager.Select())
            {
                _confirmSoundPool.Play();
            }
        }

        if (_optionIndex == 4) // Options
        {
            if (InputManager.Select())
            {
                _confirmSoundPool.Play();
                RequestPush = new OptionsScene(InputManager, GameTime, MusicManager);
            }
        }

        if (_optionIndex == 5) // Credits
        {
            if (InputManager.Select())
            {
                _confirmSoundPool.Play();
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

        // Menu setup
        Font font = FontHandler.GetFontMenu();
        
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
            Raylib.DrawTextEx(font, menuItems[reverseIndex], pos, fontSize, 1f, Color.White);

            // Draw selector arrow if selected
            if (reverseIndex == _optionIndex)
            {
                Vector2 selectorPos = new Vector2(startPos.X - 30, pos.Y);
                Raylib.DrawTextEx(font, "*", selectorPos, fontSize, 1f, Color.White);
            }
        }
    }
    public virtual void Load()
    {
        Console.WriteLine("Loading title screen...");
        _titleScreen = AssetManager.LoadTexture("title.png", 1000, 500);
    }
    public override void Unload()
    {

    }
}
