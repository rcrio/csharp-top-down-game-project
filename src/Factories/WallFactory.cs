using Raylib_cs;
using System.Collections.Generic;

public class WallFactory
{
    // Dictionary of wall templates
    public Dictionary<string, ItemPlaceable> Walls { get; private set; }

    // Class fields for textures
    public Texture2D StoneWallTexture { get; private set; }
    public Texture2D WoodenWallTexture { get; private set; }

    public WallFactory()
    {
        Load();
        Initialise();
    }

    // Load textures first
    public void Load()
    {
        StoneWallTexture = AssetManager.LoadTexture("Textures/item_placeable_stone_wall.png");
        WoodenWallTexture = AssetManager.LoadTexture("Textures/wood_wall.png");
    }

    // Initialise wall templates using loaded textures
    private void Initialise()
    {
        Walls = new Dictionary<string, ItemPlaceable>
        {
            ["stone"] = new ItemPlaceableObject("wall_stone", "Stone Wall", "Stony.", 99, StoneWallTexture),
            ["wood"] = new ItemPlaceableObject("wall_wood", "Wooden Wall", "A nice looking wooden wall.", 99, WoodenWallTexture)
        };
    }

    public void Unload()
    {
        Raylib.UnloadTexture(StoneWallTexture);
        Raylib.UnloadTexture(WoodenWallTexture);
    }
}
