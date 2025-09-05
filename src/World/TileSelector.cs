using System.Numerics;
using Raylib_cs;

public class TileSelector
{
    private World _world;
    private MousePosition _mousePosition;

    public Tile Tile { get; set; }
    private int _tileX;
    private int _tileY;
    private InputManager _inputManager;
    private Font _font;

    public TileSelector(InputManager inputManager, MousePosition mousePosition, World world, Tile tileCell = null)
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

    public void BreakFloor()
    {
        if (Tile != null && Tile._itemPlaceableFloor != null /*&& Tile.Floor.IsDestructible*/)
        {
            Tile.RemoveFloor();
        }
    }

    public void DrawInfo()
    {
        float fontSize = 16;   // desired text size for drawing
        float spacing = 1;     // optional letter spacing
        float lineHeight = fontSize + 4; // space between lines
        Vector2 pos = new Vector2(10, 10);

        if (Tile != null)
        {
            var info = Tile.TileInformation(); // call once

            Raylib.DrawTextEx(_font, "Terrain: " + info["Terrain Name"], pos, fontSize, spacing, Color.White);
            pos.Y += lineHeight;

            Raylib.DrawTextEx(_font, "Description: " + info["Terrain Description"], pos, fontSize, spacing, Color.White);
            pos.Y += lineHeight;

            Raylib.DrawTextEx(_font, "Floor: " + info["Floor Name"], pos, fontSize, spacing, Color.White);
            pos.Y += lineHeight;

            Raylib.DrawTextEx(_font, "Description: " + info["Floor Description"], pos, fontSize, spacing, Color.White);
            pos.Y += lineHeight;

            Raylib.DrawTextEx(_font, "Object/Wall: " + info["Object Name"], pos, fontSize, spacing, Color.White);
            pos.Y += lineHeight;

            Raylib.DrawTextEx(_font, "Description: " + info["Object Description"], pos, fontSize, spacing, Color.White);
        }
        else
        {
            Raylib.DrawTextEx(_font, "Impassable area", pos, fontSize, spacing, Color.White);
        }
        pos.Y += lineHeight;
        Raylib.DrawTextEx(_font, "x: " + _tileX + ", y: " + _tileY, pos, fontSize, spacing, Color.White);
    }

    public void Load(World world)
    {
        _world = world;
        _font = AssetManager.LoadFont("Fonts/Roboto-Regular.ttf", 16);
    }

    public void Unload()
    {
        AssetManager.UnloadFont(_font);
        _font = new Font();
    }
}
