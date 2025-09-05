
// Refactor later for modding support by loading item definitions from JSON files

using Raylib_cs;

public class FloorFactory
{
    // Dictionary of floor templates
    public Dictionary<string, ItemPlaceable> Floors { get; private set; }
    // Class field for the texture
    public Texture2D FloorWoodTexture { get; private set; }




    public FloorFactory()
    {

    }

    public void Load()
    {
        // Assign to the class field, not a local variable
        FloorWoodTexture = AssetManager.LoadTexture("Textures/wood_floor.png");
        Initialise();
    }

    private void Initialise()
    {
        Floors = new Dictionary<string, ItemPlaceable>
        {
            ["wood"] = new ItemPlaceableFloor("floor_wood", "Wood", "Woody.", 99, FloorWoodTexture)
        };
    }
    
    public void Unload()
    {
        Raylib.UnloadTexture(FloorWoodTexture);
    }
}
