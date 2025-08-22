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
    private bool _inventoryOpen = false;
    private InventoryWindow _inventoryWindow;
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
        _inventoryWindow = new InventoryWindow(new Vector2(100, 50), _customPlayer1.Inventory, InputManager);
    }
    public override void Update()
    {
        // -----------------------------
        // Handle Inventory Toggle
        // -----------------------------
        if (InputManager.IsActionPressed(Action.OpenInventory))
            _inventoryOpen = !_inventoryOpen;

        // -----------------------------
        // Handle Escape / Return
        // -----------------------------
        if (InputManager.IsActionPressed(Action.Return))
        {
            if (_inventoryOpen)
            {
                // Close inventory if open
                _inventoryOpen = false;
            }
            else
            {
                // Exit scene if inventory is closed
                RequestPop = true;
            }
        }

        // -----------------------------
        // Update Inventory
        // -----------------------------
        if (_inventoryOpen)
        {
            _inventoryWindow.Update();
            // Optionally: block player movement or interaction while inventory is open
        }

        // -----------------------------
        // Update Player & Camera
        // -----------------------------
        _customPlayer1.Update();

        _camera.Target = _customPlayer1.Position + new Vector2(_customPlayer1.Width / 2, _customPlayer1.Height / 2);
        _camera.Offset = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);

        if (InputManager.IsActionPressed(Action.ZoomIn)) _camera.Zoom += 0.1f;
        if (InputManager.IsActionPressed(Action.ZoomOut)) _camera.Zoom -= 0.1f;
        _camera.Zoom = Math.Clamp(_camera.Zoom, 0.5f, 3.0f);

        // -----------------------------
        // Update Mouse & Tile Selector
        // -----------------------------
        _mousePosition.MouseScreen = Raylib.GetMousePosition();
        _mousePosition.MouseWorld = Raylib.GetScreenToWorld2D(_mousePosition.MouseScreen, _camera);

        _tileSelector.Update();

        if (InputManager.IsActionPressed(Action.LeftClick) && _tileSelector.Tile != null)
        {
            _tileSelector.Tile.RemoveFloor();
        }

        if (InputManager.IsActionPressed(Action.RightClick) && _tileSelector.Tile != null)
        {
            _tileSelector.Tile.RemoveObject();
        }
    }





    public override void Draw()
    {
        Raylib.ClearBackground(Color.Black);

        // Camera Related Graphics, we draw the bottom graphics first. Think of it like a stack.
        // CAMERA MODE START ///////////////////////////////////////////////////////////////
        Raylib.BeginMode2D(_camera);

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

    }

    public override void Unload()
    {
        _customPlayer1.Unload();
    }
}