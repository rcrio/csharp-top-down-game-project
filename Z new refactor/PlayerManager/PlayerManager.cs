using System.Numerics;

public class PlayerManager
{
    private InputManager _inputManager;
    private GameTime _gameTime;
    private World _world;
    // in the future, we will use list of players
    private LocalPlayer _localPlayer;
    private InventoryWindow _inventoryWindow;
    private HotbarWindow _hotbarWindow;

    public PlayerManager(InputManager inputManager, GameTime gameTime, World world)
    {
        _inputManager = inputManager;
        _gameTime = gameTime;
        _world = world;

        _localPlayer = new LocalPlayer(new Vector2(0, 0), "nothing_yet", _gameTime, _world, 50, _inputManager);
        _inventoryWindow = new InventoryWindow(new Vector2(0, 0), _localPlayer.Inventory, _inputManager);
        _hotbarWindow = new HotbarWindow(new Vector2(0, 0), _localPlayer.Inventory, _inputManager);
    }

    public void Update()
    {

    }

    public void Draw()
    {

    }
    
    
}