using Raylib_cs;

public class WorldBuilder
{
    public World BuildDefaultWorld(int width, int height)
    {
        // Create some default items for terrain, floor, object
        // Refactor when TileFactory is setup.
        var terrain = new ItemPlaceableTerrain("Grass", "Grassy.", new Texture2D());
        var floor = new ItemPlaceableFloor("Wood", "Woody.", new Texture2D());
        var obstacle = new ItemPlaceableObject("Stone Wall", "Stony.", new Texture2D());

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