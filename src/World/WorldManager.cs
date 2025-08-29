public class WorldManager
{
    private InputManager _inputManager;
    private MousePosition _mousePosition;
    private int _worldSize;
    private WorldBuilder _worldBuilder;
    public World World { get; private set; } // Used for player manager
    private TileSelector _tileSelector;
    public WorldManager(InputManager inputManager, MousePosition mousePosition, int worldSize)
    {
        _inputManager = inputManager;
        _mousePosition = mousePosition;
        _worldSize = worldSize;
        _worldBuilder = new WorldBuilder();
        World = _worldBuilder.BuildDefaultWorld(_worldSize);
        // Make Tile cell null first.
        _tileSelector = new TileSelector(_inputManager, _mousePosition, World, null);

    }

    // We update non-constructor parameters, because constructor parameters usually get updated a level above us.
    public void Update()
    {
        _tileSelector.Update();
    }

    public void Draw() // Method gets called relative to the camera
    {
        World.Draw(_mousePosition.CameraManager.Camera);
        _tileSelector.DrawTile();
    }

    public void DrawInfo() // Method gets called statically
    {
        _tileSelector.DrawInfo();
    }
}