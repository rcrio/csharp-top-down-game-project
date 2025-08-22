// A class to store the world, to draw the world, and to check tile boundaries.
// This does NOT handle world building, WorldBuilder does that.
// WorldBuilder will populate the tiles.
using System.Numerics;

public class World
{
    // 2D Array of tiles.
    private Tile[,] _tileGrid;

    public int Width { get; private set; }
    public int Height { get; private set; }

    public World(int width, int height, Tile[,] tileGrid)
    {
        Width = width;
        Height = height;
        _tileGrid = tileGrid;
    }

    // GetTile from the 2D Array. This does not use On-screen X and On-screen Y co-ordinates.
    public Tile GetTile(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
            return null;
        return _tileGrid[x, y];
    }

    // Draws the tiles. This does not use On-screen X and On-screen Y co-ordinates.
    public void Draw()
    {
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                // We do multiply, to draw the 16x16 sprite, at the designated spot.
                _tileGrid[x, y].Draw(x * Constants.TILE_SIZE, y * Constants.TILE_SIZE);
    }

    // Document further
    public bool IsPositionWalkable(Vector2 position, int playerWidth, int playerHeight)
    {
        // Define the four corners of the player's rectangle
        // Subtract 1 from right and bottom corners to prevent 1-pixel overshoot
        Vector2 topLeft = position;
        Vector2 topRight = new Vector2(position.X + playerWidth - 1, position.Y);
        Vector2 bottomLeft = new Vector2(position.X, position.Y + playerHeight - 1);
        Vector2 bottomRight = new Vector2(position.X + playerWidth - 1, position.Y + playerHeight - 1);

        Vector2[] corners = new Vector2[] { topLeft, topRight, bottomLeft, bottomRight };

        foreach (var corner in corners)
        {
            // Out of bounds check
            if (corner.X < 0 || corner.Y < 0 || corner.X >= Width * Constants.TILE_SIZE || corner.Y >= Height * Constants.TILE_SIZE)
                return false;

            // Convert corner position to tile indices
            int tileX = (int)(corner.X / Constants.TILE_SIZE);
            int tileY = (int)(corner.Y / Constants.TILE_SIZE);

            Tile tile = _tileGrid[tileX, tileY];
            if (!tile.IsWalkable())
                return false; // Collision detected
        }

        return true; // All corners are walkable
    }



    
}
