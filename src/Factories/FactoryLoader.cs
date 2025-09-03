public class FactoryLoader
{
    public FloorFactory FloorFactory { get; private set; }
    public ItemFactory ItemFactory { get; private set; }
    public TerrainFactory TerrainFactory { get; private set; }
    public WallFactory WallFactory { get; private set; }

    public FactoryLoader()
    {
        FloorFactory = new FloorFactory();
        ItemFactory = new ItemFactory();
        TerrainFactory = new TerrainFactory();
        WallFactory = new WallFactory();
    }

    public void Load()
    {
        FloorFactory.Load();
        ItemFactory.Load();
        TerrainFactory.Load();
        WallFactory.Load();
    }

    public void Unload()
    {
        FloorFactory.Unload();
        ItemFactory.Unload();
        TerrainFactory.Unload();
        WallFactory.Unload();
    }
}
