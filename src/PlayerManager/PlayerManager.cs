using System.Numerics;

public class PlayerManager
{
    private InputManager _inputManager;
    private GameTime _gameTime;
    private MousePosition _mousePosition;
    private World _world;
    private FactoryLoader _factoryLoader;
    // in the future, we will use list of players
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

        LocalPlayer = new LocalPlayer(new Vector2(0, 0), "player_north.png", "player_south.png", "player_west.png", "player_east.png", _gameTime, _world, 50, _inputManager, _factoryLoader);
        _inventoryWindow = new InventoryWindow(new Vector2(0, 0), _inputManager, LocalPlayer.Inventory, _mousePosition);
        _hotbarWindow = new HotbarWindow(new Vector2(0, 0), LocalPlayer.Inventory, _inputManager);
    }

    public void Update()
    {
        LocalPlayer.Update(_gameTime.DeltaTime);
        _inventoryWindow.Update();
        _hotbarWindow.Update();
    }
    // We dont draw inventory
    public void Draw()
    {
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
        LocalPlayer.Load();
    }

    public void Unload()
    {
        LocalPlayer.Unload();
    }
}