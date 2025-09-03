public class WorldBuilder
{
    public FactoryLoader FactoryLoader { get; private set; }
    public WorldBuilder(FactoryLoader factoryLoader)
    {
        FactoryLoader = factoryLoader;
    }

    public World BuildDefaultWorld(int size)
    {
        int width = size;
        int height = size;
        // Create some default items for terrain, floor, object
        // Refactor terrain, floor and wall dictionary to their actual types rather than use casting.
        var terrain = (ItemPlaceableTerrain)FactoryLoader.TerrainFactory.Terrains["grass"];
        var floor = (ItemPlaceableFloor)FactoryLoader.FloorFactory.Floors["wood"];
        var obstacle = (ItemPlaceableObject)FactoryLoader.WallFactory.Walls["wood"];

        // Create a 2D array of tiles to use as a parameter for World
        Tile[,] tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(terrain, floor, null);
            }
        }

        tiles[4, 4].AddObject(obstacle);

        // Instantiate the World with the generated tile grid
        return new World(width, height, tiles);
    }
}