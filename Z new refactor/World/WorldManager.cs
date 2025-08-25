public class WorldManager
{
    private InputManager _inputManager;
    private MousePosition _mousePosition;
    private int _worldSize;
    private WorldBuilder _worldBuilder;
    public World World { get; private set; }
    private TileSelector _tileSelector;
    public WorldManager(InputManager inputManager, MousePosition mousePosition, int worldSize)
    {
        _inputManager = inputManager;
        _mousePosition = mousePosition;
        _worldSize = worldSize;
        _worldBuilder = new WorldBuilder();
        World = _worldBuilder.BuildDefaultWorld(_worldSize);
        // Make Tile cell null first.
        _tileSelector = new TileSelector(inputManager, mousePosition, World, null);

    }

    // We update non-constructor parameters, because constructor parameters usually get updated a level above us.
    public void Update()
    {

    }

    public void Draw()
    {
        World.Draw();
    }
}