public class WorldManager
{
    private InputManager _inputManager;
    private GameTime _gameTime;
    private MousePosition _mousePosition;
    private int _worldSize;
    private FactoryLoader _factoryLoader;
    private WorldBuilder _worldBuilder;
    public World World { get; private set; } // Used for player manager
    public WorldManager(InputManager inputManager, GameTime gameTime, MousePosition mousePosition, FactoryLoader factoryLoader, int worldSize)
    {
        _inputManager = inputManager;
        _gameTime = gameTime;
        _mousePosition = mousePosition;
        _worldSize = worldSize;

        _factoryLoader = factoryLoader;
        _worldBuilder = new WorldBuilder(_factoryLoader);
        World = _worldBuilder.BuildDefaultWorld(_worldSize);
    }

    // We update non-constructor parameters, because constructor parameters usually get updated a level above us.
    public void Update()
    {
        World.Update(_gameTime.DeltaTime);
    }

    public void DrawBeforePlayer() // Method gets called relative to the camera
    {
        World.DrawBeforePlayer(_mousePosition.CameraManager.Camera);
    }

    public void DrawAfterPlayer()
    {
        World.DrawAfterPlayer(_mousePosition.CameraManager.Camera);
    }

    
    public void Load()
    {
        
    }

    public void Unload()
    {
        
    }
}