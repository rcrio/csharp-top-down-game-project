using System.Numerics;
using Raylib_cs;

public class GameScene : Scene
{
    // InputManager and GameTime inherited from Scene
    private GameClock _gameClock;
    private CameraManager _cameraManager;
    private MousePosition _mousePosition;

    private WorldManager _worldManager;
    private PlayerManager _playerManager;
    private bool _inventoryOpen = false;

    public GameScene(InputManager inputManager, GameTime gameTime)
    {
        // reorganise variables
        InputManager = inputManager;
        GameTime = gameTime;
        _gameClock = new GameClock();

        _cameraManager = new CameraManager(new Vector2(400, 400), new Vector2(0, 0), inputManager);

        _mousePosition = new MousePosition(_cameraManager);

        _worldManager = new WorldManager(inputManager, _mousePosition, 1000); // Later on, add gameTime

        _playerManager = new PlayerManager(inputManager, gameTime, _worldManager.World);
    }
    public override void Update()
    {
        // Delta time
        GameTime.Update();                     // updates delta time

        // Game clock update
        _gameClock.Update(GameTime.DeltaTime); // ticks in-game clock

        // Update camera, could refactor
        var target = _playerManager.LocalPlayer.Center;
        var offset = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);
        _cameraManager.Update(offset, target);

        // Update mouse
        _mousePosition.Update(_cameraManager);

        // Update world
        _worldManager.Update();

        // Update player
        _playerManager.Update();

        // Handle escape and return. This is temporary and we will have an escape menu to save and exit.
        // Must be before inventory logic
        if (InputManager.Return() && !_inventoryOpen) RequestPop = true;
        
        if (InputManager.OpenInventory()) _inventoryOpen = !_inventoryOpen;

        if (_inventoryOpen && InputManager.Return()) _inventoryOpen = false;
    }

    public override void Draw()
    {
        // HUD
        _gameClock.DrawClock();

        // Camera Related Graphics, we draw the bottom graphics first. Think of it like a stack.
        Raylib.BeginMode2D(_cameraManager.Camera);

        // Draw the world
        _worldManager.Draw();

        // Draw tile selector
        _playerManager.Draw();

        Raylib.EndMode2D(); // Camera graphics end here

        // Tile information
        if (_inventoryOpen)
        {
            _playerManager.InventoryDraw();
        }

        _worldManager.DrawInfo();

        _playerManager.HotbarDraw();

        _gameClock.DrawClock();
    }

    public override void Unload()
    {
        _playerManager.Unload();
    }
}