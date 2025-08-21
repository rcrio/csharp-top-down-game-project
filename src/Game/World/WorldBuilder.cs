using Raylib_cs;

public class WorldBuilder
{
    public World BuildDefaultWorld(int width, int height)
    {
        // Create a 2D array of tiles to use as a parameter for World
        Tile[,] tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Create some default items for terrain, floor, object
                var terrain = new ItemPlaceableTerrain("Grass", "Grassy.", new Texture2D());

                tiles[x, y] = new Tile(terrain, null, null);
            }
        }

        // Instantiate the World with the generated tile grid
        return new World(width, height, tiles);
    }
}