public class WorldBuilder
{
    public World BuildDefaultWorld(int size)
    {
        int width = size;
        int height = size;
        // Create some default items for terrain, floor, object
        // Refactor PlaceableItemFactory, at the moment we are using the Upper class to make ItemPlaceable.
        var terrain = (ItemPlaceableTerrain)TerrainFactory.Terrains["terrain_grass"];
        var floor = (ItemPlaceableFloor)FloorFactory.Floors["floor_wood"];
        var obstacle = (ItemPlaceableObject)WallFactory.Walls["wall_stone"];

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