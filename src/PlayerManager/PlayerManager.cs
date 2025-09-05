using System.Numerics;

public class PlayerManager
{
    private InputManager _inputManager;
    private GameTime _gameTime;
    private MousePosition _mousePosition;
    private World _world;
    private FactoryLoader _factoryLoader;
    // in the future, we will use list of players
    private TileSelector _tileSelector;
    public LocalPlayer LocalPlayer { get; private set; }
    private InventoryWindow _inventoryWindow;
    private HotbarWindow _hotbarWindow;

    public PlayerManager(InputManager inputManager, GameTime gameTime, MousePosition mousePosition, World world, FactoryLoader factoryLoader)
    {
        _inputManager = inputManager;
        _gameTime = gameTime;
        _mousePosition = mousePosition;
        _world = world;
        _factoryLoader = factoryLoader;

        _tileSelector = new TileSelector(_inputManager, _mousePosition, _world, null);
        LocalPlayer = new LocalPlayer(
            new Vector2(0, 0),
            "Textures/player_north.png",
            "Textures/player_south.png",
            "Textures/player_west.png",
            "Textures/player_east.png",
            _gameTime,
            _world,
            50,
            _inputManager,
            _factoryLoader,
            _tileSelector);
        _inventoryWindow = new InventoryWindow(new Vector2(0, 0), _inputManager, LocalPlayer.Inventory, _mousePosition);
        _hotbarWindow = new HotbarWindow(new Vector2(0, 0), LocalPlayer.Inventory, _inputManager);
    }

    public void Update()
    {
        _tileSelector.Update();
        LocalPlayer.Update(_gameTime.DeltaTime);
        _inventoryWindow.Update();
        _hotbarWindow.Update();
    }

    // We dont draw inventory
    public void Draw()
    {
        _tileSelector.DrawTile();
        LocalPlayer.Draw();
    }

    public void InventoryDraw()
    {
        _inventoryWindow.Draw();
    }

    public void HotbarDraw()
    {
        _hotbarWindow.Draw();
    }

    public void Load()
    {
        _tileSelector.Load();
        LocalPlayer.Load();
    }

    public void Unload()
    {
        _tileSelector.Unload();
        LocalPlayer.Unload();
    }

    public void DrawInfo()
    {
        _tileSelector.DrawInfo();
    }

}