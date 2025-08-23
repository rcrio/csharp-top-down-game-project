using Raylib_cs;

public class TileSelector
{
    private World _world;
    private MousePosition _mousePosition;

    public Tile Tile { get; set; }
    private int _tileX;
    private int _tileY;
    private InputManager _inputManager;

    public TileSelector(World world, MousePosition mousePosition, InputManager inputManager, Tile tileCell = null)
    {
        _world = world;
        _mousePosition = mousePosition;
        _inputManager = inputManager;
        Tile = tileCell;
        _tileX = (int)(_mousePosition.MouseWorld.X / Constants.TILE_SIZE);
        _tileY = (int)(_mousePosition.MouseWorld.Y / Constants.TILE_SIZE);
    }

    public void Update()
    {
        _tileX = (int)(_mousePosition.MouseWorld.X / Constants.TILE_SIZE);
        _tileY = (int)(_mousePosition.MouseWorld.Y / Constants.TILE_SIZE);
        Tile = _world.GetTile(_tileX, _tileY);
        ChangeTile();
    }

    public void DrawTile()
    {
        if (Tile != null)
        {
            Raylib.DrawRectangleLinesEx(
                new Rectangle(_tileX * Constants.TILE_SIZE,
                _tileY * Constants.TILE_SIZE,
                Constants.TILE_SIZE,
                Constants.TILE_SIZE),
                2,
                Color.Gold
            );
        }
    }

    public void DrawInfo()
    {
        if (Tile != null)
        {
            string terrainName = Tile.TileInformation()["Terrain Name"];
            string terrainDesc = Tile.TileInformation()["Terrain Description"];
            string floorName = Tile.TileInformation()["Floor Name"];
            string floorDesc = Tile.TileInformation()["Floor Description"];
            string objectName = Tile.TileInformation()["Object Name"];
            string objectDesc = Tile.TileInformation()["Object Description"];
            Raylib.DrawText("Terrain: " + terrainName, 10, 10, 10, Color.White);
            Raylib.DrawText("Description: " + terrainDesc, 10, 20, 10, Color.White);
            Raylib.DrawText("Floor: " + floorName, 10, 30, 10, Color.White);
            Raylib.DrawText("Description: " + floorDesc, 10, 40, 10, Color.White);
            Raylib.DrawText("Object/Wall: " + objectName, 10, 50, 10, Color.White);
            Raylib.DrawText("Description: " + objectDesc, 10, 60, 10, Color.White);
        }
        else
        {
            Raylib.DrawText("Impassable area", 10, 10, 10, Color.White);
        }
    }

    public void ChangeTile()
    {
        if (_inputManager.IsActionDown(Action.LeftClick) && Tile != null)
        {
            Tile.RemoveFloor();
        }

        if (_inputManager.IsActionPressed(Action.RightClick) && Tile != null)
        {
            Tile.RemoveObject();
        }
    }
}
