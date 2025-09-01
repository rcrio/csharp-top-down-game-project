using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

public class MainMenuScene : Scene
{
    // InputManager and GameTime inherited from Scene
    private int _optionIndex;
    private int _optionAmount;
    private int _selectorPosY;
    private Texture2D _titleScreen;
    private Font _thisFont;

    // Constructor for the Main Menu Scene
    public MainMenuScene(InputManager inputManager, GameTime gameTime, MusicManager musicManager)
    {
        InputManager = inputManager;
        GameTime = gameTime;
        MusicManager = musicManager;
        _optionIndex = 0;
        _optionAmount = 3;
        _selectorPosY = 20;
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
        }

        if (InputManager.MoveDownSelect() || InputManager.ArrowDown())
        {
            _optionIndex = (_optionIndex + 1) % _optionAmount;
        }

        // Scene changing or exit
        if (_optionIndex == 0) // Play
        {
            _selectorPosY = 20;
            if (InputManager.Select())
            {
                RequestPush = new GameScene(InputManager, GameTime, MusicManager);
            }
        }

        if (_optionIndex == 1) // Options
        {
            _selectorPosY = 50;
            if (InputManager.Select())
            {
                RequestPush = new OptionsScene(InputManager, GameTime, MusicManager);
            }
        }

        if (_optionIndex == 2) // Exit
        {
            _selectorPosY = 80;
            if (InputManager.Select()) RequestExit = true;
        }
    }

    public override void Draw()
    {
        Raylib.ClearBackground(Color.Black);

        int screenWidth = Raylib.GetScreenWidth();
        int screenHeight = Raylib.GetScreenHeight();

        int posX = (screenWidth / 2) - (_titleScreen.Width / 2);
        int posY = 20; // Small padding from top

        Raylib.DrawTexture(_titleScreen, posX, posY, Color.White);
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
