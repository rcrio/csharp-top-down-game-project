// A class to store the world, to draw the world, and to check tile boundaries.
// This does NOT handle world building, WorldBuilder does that.
// WorldBuilder will populate the tiles.
using System.Numerics;
using Raylib_cs;

public class World
{
    // 2D Array of tiles.
    private Tile[,] _tileGrid;

    public int Width { get; private set; }
    public int Height { get; private set; }
    // could refactor by only passing in Camera, look at CameraManager and see if world needs anything from CameraManager
    private CameraManager CameraManager { get; set; }

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
    public void Draw(Camera2D camera)
{
    // Get the visible region in world coordinates
    Rectangle cameraView = new Rectangle(
        camera.Target.X - camera.Offset.X,
        camera.Target.Y - camera.Offset.Y,
        Raylib.GetScreenWidth(),
        Raylib.GetScreenHeight()
    );

    int tileSize = Constants.TILE_SIZE; // whatever your tile width/height is

    // Convert camera view into tile index range
    int startX = Math.Max(0, (int)(cameraView.X / tileSize));
    int startY = Math.Max(0, (int)(cameraView.Y / tileSize));
    int endX = Math.Min(Width, (int)((cameraView.X + cameraView.Width) / tileSize) + 1);
    int endY = Math.Min(Height, (int)((cameraView.Y + cameraView.Height) / tileSize) + 1);

    // Loop only over tiles on-screen
    for (int x = startX; x < endX; x++)
    {
        for (int y = startY; y < endY; y++)
        {
            _tileGrid[x, y].Draw(x * Constants.TILE_SIZE, y * Constants.TILE_SIZE);
        }
    }
}

    // Document further
    // Check if a rectangular area (in tiles) is walkable
    public bool IsBoxWalkable(Rectangle rect)
    {
        // Convert rectangle bounds to tile indices
        int startX = (int)(rect.X / Constants.TILE_SIZE);
        int startY = (int)(rect.Y / Constants.TILE_SIZE);
        // Prevents tile overcreep because pixel coordinates are 0-indexed
        int endX = (int)((rect.X + rect.Width - 1) / Constants.TILE_SIZE);
        int endY = (int)((rect.Y + rect.Height - 1) / Constants.TILE_SIZE);

        // Check all tiles covered by the rectangle
        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                // Out of bounds check
                if (x < 0 || y < 0 || x >= Width || y >= Height)
                    return false;

                if (!_tileGrid[x, y].IsWalkable())
                    return false; // collision with wall or obstacle
            }
        }

        // All tiles are walkable
        return true;
    }

}
