using System.Numerics;
using Raylib_cs;

public class GameScene : Scene
{
    // InputManager and GameTime inherited from Scene
    private GameClock _gameClock;
    private WorldBuilder _worldBuilder;
    private World _world;
    private LocalPlayer _customPlayer1;
    private MousePosition _mousePosition;
    private TileSelector _tileSelector;
    private bool _inventoryOpen = false;
    private InventoryWindow _inventoryWindow;
    private HotbarWindow _hotbarWindow;
    private CameraManager _cameraManager;

    private WorldManager _worldManager;

    public GameScene(InputManager inputManager, GameTime gameTime)
    {
        // reorganise variables
        InputManager = inputManager;
        GameTime = gameTime;
        _gameClock = new GameClock();

        _cameraManager = new CameraManager(new Vector2(400, 400), new Vector2(0, 0), inputManager);
        _mousePosition = new MousePosition(_cameraManager.Camera);

        _worldManager = new WorldManager(inputManager, _mousePosition, 1000); // Later on, add gameTime

        _playerManager =

        _customPlayer1 = new CustomPlayer1(new Vector2(0, 0), InputManager, GameTime, _world, new Texture2D());
        _inventoryWindow = new InventoryWindow(new Vector2(100, 50), _customPlayer1.Inventory, InputManager);
        _hotbarWindow = new HotbarWindow(new Vector2(100, Raylib.GetScreenHeight() - 80), _customPlayer1.Inventory, InputManager);



    }
    public override void Update()
    {
        // Time
        GameTime.Update();                     // updates delta time
        _gameClock.Update(GameTime.DeltaTime); // ticks in-game clock
        // Handle escape and return. This is temporary and we will have an escape menu to save and exit.
        // Must be before inventory logic
        if (InputManager.Return() && !_inventoryOpen) RequestPop = true;


        // Inventory management, may need an InventoryManager in the future.
        // can move this, pass screenwidth? or use it
        
        
        if (InputManager.OpenInventory())
        {
            _inventoryOpen = !_inventoryOpen;
        }

        if (_inventoryOpen)
        {
            _inventoryWindow.Update();
            if (InputManager.Return()) _inventoryOpen = false;
            // Optionally: block player movement or interaction while inventory is open
        }


        // Update player
        _customPlayer1.Update();


        // Update camera
        var target = _customPlayer1.Position + _customPlayer1.Center;
        var offset = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);
        _cameraManager.Update(offset, target);


        // Update mouse
        _mousePosition.Update(_cameraManager.Camera);


        // Update tile, must be after mouse
        _tileSelector.Update();

    
        // Hotbar update
        _hotbarWindow.Update();
    }

    public override void Draw()
    {
        // Camera Related Graphics, we draw the bottom graphics first. Think of it like a stack.
        Raylib.BeginMode2D(_cameraManager.Camera);

        // Draw the world
        _world.Draw(_cameraManager.Camera);

        // Draw tile selector
        _tileSelector.DrawTile();

        // Draw the player
        _customPlayer1.Draw();

        Raylib.EndMode2D(); // Camera graphics end here

        // Tile information
        _tileSelector.DrawInfo();
        if (_inventoryOpen)
        {
            _inventoryWindow.Draw();
        }
        _hotbarWindow.Draw();

        // HUD
        _gameClock.DrawClock();
    }

    public override void Unload()
    {
        _customPlayer1.Unload();
    }
}