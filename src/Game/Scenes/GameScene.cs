using System.Numerics;
using Raylib_cs;

public class GameScene : Scene
{
    // InputManager and GameTime inherited from Scene

    private WorldBuilder _worldBuilder;
    private World _world;
    private CustomPlayer1 _customPlayer1;
    private Camera2D _camera;
    private MousePosition _mousePosition;
    private TileSelector _tileSelector;
    public GameScene(InputManager inputManager, GameTime gameTime)
    {
        // Temp variables to populate MousePosition
        Vector2 mouseScreen = Raylib.GetMousePosition();
        Vector2 mouseWorld = Raylib.GetScreenToWorld2D(mouseScreen, _camera);


        InputManager = inputManager;
        GameTime = gameTime;
        _worldBuilder = new WorldBuilder();
        _world = _worldBuilder.BuildDefaultWorld(Constants.WORLD_SIZE, Constants.WORLD_SIZE);
        _customPlayer1 = new CustomPlayer1(new Vector2(0, 0), InputManager, GameTime, _world, new Texture2D());
        _camera = new Camera2D
        {
            Target = _customPlayer1.Position,
            Offset = new Vector2(400, 300),
            Rotation = 0.0f,
            Zoom = 1.0f
        };
        _mousePosition = new MousePosition(mouseScreen, mouseWorld);
        _tileSelector = new TileSelector(_world, _mousePosition, null);
    }

    public override void Update()
    {
        // Update player
        _customPlayer1.Update();

        // Update camera to follow player
        // We divide by 2 to get the center to focus on.
        _camera.Target = _customPlayer1.Position + new Vector2(_customPlayer1.Width / 2, _customPlayer1.Height / 2);
        _camera.Offset = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);

        // Zoom logic
        if (InputManager.IsActionPressed(Action.ZoomIn))  _camera.Zoom += 0.1f; // scroll in
        if (InputManager.IsActionPressed(Action.ZoomOut)) _camera.Zoom -= 0.1f; // scroll out
        
        // Zoom boundaries, so we dont zoom too far in or out
        if (_camera.Zoom <= 0.5f) _camera.Zoom = 0.5f;
        if (_camera.Zoom >= 3.0f) _camera.Zoom = 3.0f;

        // Mouse update
        _mousePosition.MouseScreen = Raylib.GetMousePosition();
        _mousePosition.MouseWorld = Raylib.GetScreenToWorld2D(_mousePosition.MouseScreen, _camera); // convert for camera

        // Tile selector
        _tileSelector.Update();

        // Input manager to go back from game
        if (InputManager.IsActionPressed(Action.Return))
        {
            RequestPop = true;
        }
    }

    public override void Draw()
    {
        Raylib.ClearBackground(Color.Black);


        // Camera Related Graphics, we draw the bottom graphics first. Think of it like a stack.
        Raylib.BeginMode2D(_camera);

        // Draw the world
        _world.Draw();

        // Draw the player
        _customPlayer1.Draw();

        _tileSelector.DrawTile();
        Raylib.EndMode2D();
        // Camera mode ends


        // Static drawing
        // Tile information
        _tileSelector.DrawInfo();

    }

    public override void Unload()
    {
        _customPlayer1.Unload();
    }

    
}