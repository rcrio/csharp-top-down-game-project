
// Refactor later for modding support by loading item definitions from JSON files

using Raylib_cs;

public class TerrainFactory
{
    // Dictionary of floor templates
    public Dictionary<string, ItemPlaceable> Terrains { get; private set; }
    // Class field for the texture
    public Texture2D GrassTerrainTexture { get; private set; }

    public TerrainFactory()
    { 

    }

    // Firstly, load textures
    public void Load()
    {
        // Assign to the class field, not a local variable
        GrassTerrainTexture = AssetManager.LoadTexture("Textures/terrain_grass.png");
        Initialise();
    }

    // Secondly, initialise item templates using the loaded textures
    private void Initialise()
    {
        Terrains = new Dictionary<string, ItemPlaceable>
        {
            ["grass"] = new ItemPlaceableTerrain("item_placeable_terrain_grass", "Grass", "Grass. Oooo. I love grass. Said by some cow probably.", 1, GrassTerrainTexture),
        };
    }

    public void Unload()
    {
        Raylib.UnloadTexture(GrassTerrainTexture);
    }
}