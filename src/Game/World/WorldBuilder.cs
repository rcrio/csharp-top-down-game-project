using Raylib_cs;

public class WorldBuilder
{
    public World BuildDefaultWorld(int width, int height)
    {
        // Create some default items for terrain, floor, object
        // Refactor when TileFactory is setup.
        var terrain = new ItemPlaceableTerrain("terrain_grass", "Grass", "Grassy.");
        var floor = new ItemPlaceableFloor("terrain_wood", "Wood", "Woody.");
        var obstacle = new ItemPlaceableObject("wall_stone", "Stone Wall", "Stony.");

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