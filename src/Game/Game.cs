using Raylib_cs;

// Main game class that creates a gameTime, windowManager, inputManager and sceneManager.
// Window initialisation happens here and ends here.
// Main game logic happens in GameScene, which is managed by SceneManager.
public class Game
{
    private WindowManager _windowManager;
    private GameTime _gameTime;
    private InputManager _inputManager;
    private SceneManager _sceneManager;

    // Constructor
    public Game()
    {
        _windowManager = new WindowManager(1600, 900, 144);
        _gameTime = new GameTime();
        _inputManager = new InputManager();
        _sceneManager = new SceneManager(_inputManager, _gameTime);
    }

    // Initalise the window, and the game loop then runs
    public void Run()
    {
        // Initialise the window
        _windowManager.Init();

        // Main game loop
        while (!Raylib.WindowShouldClose() && !_sceneManager.ShouldClose())
        {
            // Update
            _gameTime.DeltaTime = Raylib.GetFrameTime();
            _sceneManager.Update();

            // Draw
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            _sceneManager.Draw();
            Raylib.EndDrawing();
        }

        // Game loop exited, now raylib closes
        Raylib.CloseWindow();
    }
}