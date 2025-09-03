public class WorldManager
{
    private InputManager _inputManager;
    private GameTime _gameTime;
    private MousePosition _mousePosition;
    private int _worldSize;
    private FactoryLoader _factoryLoader;
    private WorldBuilder _worldBuilder;
    public World World { get; private set; } // Used for player manager
    private TileSelector _tileSelector;
    public WorldManager(InputManager inputManager, GameTime gameTime, MousePosition mousePosition, FactoryLoader factoryLoader, int worldSize)
    {
        _inputManager = inputManager;
        _gameTime = gameTime;
        _mousePosition = mousePosition;
        _worldSize = worldSize;

        _factoryLoader = factoryLoader;
        _worldBuilder = new WorldBuilder(_factoryLoader);
        World = _worldBuilder.BuildDefaultWorld(_worldSize);
        _tileSelector = new TileSelector(_inputManager, _mousePosition, World, null);
    }

    // We update non-constructor parameters, because constructor parameters usually get updated a level above us.
    public void Update()
    {
        World.Update(_gameTime.DeltaTime);
        _tileSelector.Update();
    }

    public void DrawBeforePlayer() // Method gets called relative to the camera
    {
        World.DrawBeforePlayer(_mousePosition.CameraManager.Camera);
        _tileSelector.DrawTile();
    }

    public void DrawAfterPlayer()
    {
        World.DrawAfterPlayer(_mousePosition.CameraManager.Camera);
    }

    public void DrawInfo() // Method gets called statically
    {
        _tileSelector.DrawInfo();
    }

    public void Load()
    {
        _tileSelector.Load();
    }

    public void Unload()
    {
        _tileSelector.Unload();
    }
}