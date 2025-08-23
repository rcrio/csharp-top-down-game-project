using System.Numerics;
using Raylib_cs;

public class GameScene : Scene
{
    // InputManager and GameTime inherited from Scene

    private WorldBuilder _worldBuilder;
    private World _world;
    private CustomPlayer1 _customPlayer1;
    private MousePosition _mousePosition;
    private TileSelector _tileSelector;
    private bool _inventoryOpen = false;
    private InventoryWindow _inventoryWindow;
    private HotbarWindow _hotbarWindow;
    private CameraManager _cameraManager;

    public GameScene(InputManager inputManager, GameTime gameTime)
    {



        InputManager = inputManager;
        GameTime = gameTime;
        _worldBuilder = new WorldBuilder();
        _world = _worldBuilder.BuildDefaultWorld(Constants.WORLD_SIZE, Constants.WORLD_SIZE);
        _customPlayer1 = new CustomPlayer1(new Vector2(0, 0), InputManager, GameTime, _world, new Texture2D());
        // refactor by creating it in SceneManager and then passing it to Scenes



        _inventoryWindow = new InventoryWindow(new Vector2(100, 50), _customPlayer1.Inventory, InputManager);
        _hotbarWindow = new HotbarWindow(new Vector2(100, Raylib.GetScreenHeight() - 80), _customPlayer1.Inventory, InputManager);
        _cameraManager = new CameraManager(new Vector2(400, 400), _customPlayer1.Position, inputManager);

        // Temp variables to populate MousePosition
        _mousePosition = new MousePosition(_cameraManager.Camera);
        _tileSelector = new TileSelector(_world, _mousePosition, null);
    }
    public override void Update()
    {
        // Inventory management, may need an InventoryManager in the future.
        if (InputManager.IsActionPressed(Action.OpenInventory))
        {
            _inventoryOpen = !_inventoryOpen;
        }

        if (_inventoryOpen)
        {
            _inventoryWindow.Update();
            if (InputManager.IsActionPressed(Action.Return)) _inventoryOpen = false;
            // Optionally: block player movement or interaction while inventory is open
        }

        // Update plauer
        _customPlayer1.Update();

        // Update camera
        var target = _customPlayer1.Position + _customPlayer1.Center;
        var offset = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);
        _cameraManager.Update(offset, target);

        // Handle escape and return. This is temporary and we will have an escape menu to save and exit.
        if (InputManager.IsActionPressed(Action.Return) && !_inventoryOpen) RequestPop = true;

        // Tile selector and mouse. Needs refactoring, mousePosition.update just needs to be called, and tileselector needs inputmanager to be passed through.
        _mousePosition.Update(_cameraManager.Camera);

        _tileSelector.Update();

        if (InputManager.IsActionDown(Action.LeftClick) && _tileSelector.Tile != null)
        {
            _tileSelector.Tile.RemoveFloor();
        }

        if (InputManager.IsActionPressed(Action.RightClick) && _tileSelector.Tile != null)
        {
            _tileSelector.Tile.RemoveObject();
        }

        // hotbar update
        _hotbarWindow.Update();
    }





    public override void Draw()
    {
        Raylib.ClearBackground(Color.Black);

        // Camera Related Graphics, we draw the bottom graphics first. Think of it like a stack.
        // CAMERA MODE START ///////////////////////////////////////////////////////////////
        Raylib.BeginMode2D(_cameraManager.Camera);

        // Draw the world
        _world.Draw();

        // Draw tile selector
        _tileSelector.DrawTile();

        // Draw the player
        _customPlayer1.Draw();

        Raylib.EndMode2D();

        // CAMERA MODE ENDS /////////////////////////////////////////////////////////////////

        // Static drawing //

        // Tile information
        _tileSelector.DrawInfo();
        if (_inventoryOpen)
        {
            // In your GameScene Draw() or after initializing _inventoryWindow
            _inventoryWindow.Position = new Vector2(
                Raylib.GetScreenWidth() - _inventoryWindow.Size.X - 10, // 10px padding from right
                10 // 10px padding from top
            );
            _inventoryWindow.Draw();
        }
        _hotbarWindow.Draw();

    }

    public override void Unload()
    {
        _customPlayer1.Unload();
    }
}