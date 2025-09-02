// A class to store the world, to draw the world, and to check tile boundaries.
// This does NOT handle world building, WorldBuilder does that.
// WorldBuilder will populate the tiles.
using Raylib_cs;

public class World
{
    // 2D Array of tiles.
    private Tile[,] _tileGrid;
    public int Width { get; private set; }
    public int Height { get; private set; }
    public DroppedItemManager DroppedItemManager { get; private set; }

    public World(int width, int height, Tile[,] tileGrid)
    {
        Width = width;
        Height = height;
        _tileGrid = tileGrid;
        DroppedItemManager = new DroppedItemManager();
    }

    // GetTile from the 2D Array. This does not use On-screen X and On-screen Y co-ordinates.
    public Tile GetTile(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
            return null;
        return _tileGrid[x, y];
    }

    public void Update(float deltaTime)
    {
        DroppedItemManager.Update(deltaTime);
    }
    // Draws the tiles. This does not use On-screen X and On-screen Y co-ordinates.
    public void DrawBeforePlayer(Camera2D camera)
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
        int startX = Math.Max(0, (int)Math.Floor(cameraView.X / tileSize));
        int startY = Math.Max(0, (int)Math.Floor(cameraView.Y / tileSize));
        int endX = Math.Min(Width, (int)Math.Floor((cameraView.X + cameraView.Width) / tileSize) + 1);
        int endY = Math.Min(Height, (int)Math.Floor((cameraView.Y + cameraView.Height) / tileSize) + 1);

        // Loop only over tiles on-screen
        for (int x = startX; x < endX; x++)
        {
            for (int y = startY; y < endY; y++)
            {
                _tileGrid[x, y].DrawBeforePlayer(x * Constants.TILE_SIZE, y * Constants.TILE_SIZE);
            }
        }
        DroppedItemManager.Draw();
    }

    public void DrawAfterPlayer(Camera2D camera)
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
        int startX = Math.Max(0, (int)Math.Floor(cameraView.X / tileSize));
        int startY = Math.Max(0, (int)Math.Floor(cameraView.Y / tileSize));
        int endX = Math.Min(Width, (int)Math.Floor((cameraView.X + cameraView.Width) / tileSize) + 1);
        int endY = Math.Min(Height, (int)Math.Floor((cameraView.Y + cameraView.Height) / tileSize) + 1);

        // Loop only over tiles on-screen
        for (int x = startX; x < endX; x++)
        {
            for (int y = startY; y < endY; y++)
            {
                _tileGrid[x, y].DrawAfterPlayer(x * Constants.TILE_SIZE, y * Constants.TILE_SIZE);
            }
        }
    }

    // Document further
    // Check if a rectangular area (in tiles) is walkable
    public bool IsBoxWalkable(Rectangle rect)
    {
        // Convert rectangle bounds to tile indices
        int startX = (int)Math.Floor(rect.X / Constants.TILE_SIZE);
        int startY = (int)Math.Floor(rect.Y / Constants.TILE_SIZE);
        // Compute inclusive end indices: use Ceiling to ensure any partial overlap counts
        int endX = (int)Math.Ceiling((rect.X + rect.Width) / Constants.TILE_SIZE) - 1;
        int endY = (int)Math.Ceiling((rect.Y + rect.Height) / Constants.TILE_SIZE) - 1;

        // Loop through all tiles in the map and check if they are walkable
        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                // Outside of map borders
                if (x < 0 || y < 0 || x >= Width || y >= Height)
                    return false;

                // Not walkable
                if (!_tileGrid[x, y].IsWalkable())
                    return false;
            }
        }

        // All tiles are walkable
        return true;
    }

    public void AddDroppedItem(ItemStack itemStack, int x, int y)
    {

    }

    // probably wont be void
    public void CollectDroppedItem()
    {

    }
}
