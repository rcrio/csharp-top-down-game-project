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
    private int _selectorPosY;
    private Texture2D _titleScreen;
    private Font _thisFont;
    private SoundPool _selectSoundPool;
    private SoundPool _confirmSoundPool;
    

    // Constructor for the Main Menu Scene
    public MainMenuScene(InputManager inputManager, GameTime gameTime, MusicManager musicManager) : base(inputManager, gameTime, musicManager)
    {
        _optionIndex = 0;
        _optionAmount = 7;
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
            _selectorPosY = 20;
            if (InputManager.Select())
            {
                _confirmSoundPool.Play();
                RequestPush = new GameScene(InputManager, GameTime, MusicManager);
            }
        }

        if (_optionIndex == 1) // Load Game
        {
            _selectorPosY = 50;
            if (InputManager.Select())
            {
                _confirmSoundPool.Play();
            }
        }

        if (_optionIndex == 2) // Multiplayer
        {
            _selectorPosY = 80;
            if (InputManager.Select())
            {
                _confirmSoundPool.Play();
            }
        }

        if (_optionIndex == 3) // Stats
        {
            _selectorPosY = 110;
            if (InputManager.Select())
            {
                _confirmSoundPool.Play();
            }
        }

        if (_optionIndex == 4) // Options
        {
            _selectorPosY = 140;
            if (InputManager.Select())
            {
                _confirmSoundPool.Play();
                RequestPush = new OptionsScene(InputManager, GameTime, MusicManager);
            }
        }

        if (_optionIndex == 5) // Credits
        {
            _selectorPosY = 170;
            if (InputManager.Select())
            {
                _confirmSoundPool.Play();
            }
        }

        if (_optionIndex == 6) // Exit
        {
            _selectorPosY = 200;
            if (InputManager.Select())
            {
                RequestExit = true;
            }
        }
    }

    public override void Draw()
    {
        Raylib.ClearBackground(Color.Black);

        int screenWidth = Raylib.GetScreenWidth();
        int screenHeight = Raylib.GetScreenHeight();

        // Center the title horizontally, small padding at top
        int posX = (screenWidth / 2) - (_titleScreen.Width / 2);
        int posY = 20;

        Raylib.DrawTexture(_titleScreen, posX, posY, Color.White);

        Font font = FontHandler.GetFontMenu();
        float fontSize = 48;
        float spacing = 1f;
        float lineHeight = fontSize + 10;

        string[] menuItems = { "New Game", "Load Game", "Multiplayer", "Stats", "Options", "Credits", "Exit" };

        // Total height of all menu items
        float totalHeight = menuItems.Length * lineHeight;

        // Start drawing so the last item ("Exit") ends up 20px above bottom
        Vector2 startPos = new Vector2(50, screenHeight - totalHeight - 20);

        for (int i = 0; i < menuItems.Length; i++)
        {
            // Normal order: New Game first, Exit last
            Vector2 pos = new Vector2(startPos.X, startPos.Y + i * lineHeight);
            Raylib.DrawTextEx(font, menuItems[i], pos, fontSize, spacing, Color.White);

            // Draw arrow next to the selected option
            if (i == _optionIndex)
            {
                Vector2 selectorPos = new Vector2(startPos.X - 30, pos.Y);
                Raylib.DrawTextEx(font, "*", selectorPos, fontSize, spacing, Color.White);
            }
        }
    }



    public virtual void Load()
    {
        Console.WriteLine("Loading title screen...");
        _titleScreen = AssetManager.LoadTitleTexture("title.png");
        _thisFont = AssetManager.LoadFont("ferrum.otf");
    }
    public override void Unload()
    {

    }
}
