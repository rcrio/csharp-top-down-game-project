
// Refactor later for modding support by loading item definitions from JSON files

using Raylib_cs;

public class ItemFactory
{
    // Dictionary of floor templates
    public Dictionary<string, ItemRegular> Items { get; private set; }
    // Class field for the texture
    public Texture2D PickaxeWoodTexture { get; private set; }
    public Texture2D SwordWoodTexture { get; private set; }

    public ItemFactory()
    { 

    }

    // Firstly, load textures
    public void Load()
    {
        // Assign to the class field, not a local variable
        PickaxeWoodTexture = AssetManager.LoadTexture("Textures/wood_pickaxe.png");
        SwordWoodTexture = AssetManager.LoadTexture("Textures/wood_sword.png");
        Initialise();
    }

    // Secondly, initialise item templates using the loaded textures
    private void Initialise()
    {
        Items = new Dictionary<string, ItemRegular>
        {
            ["wood_pickaxe"] = new ItemRegular("item_regular_wood_pickaxe", "Wood Pickaxe", "A wood pickaxe.", 1, PickaxeWoodTexture),
            ["wood_sword"] = new ItemRegular("item_regular_wood_sword", "Wood Sword", "A wood sword.", 1, SwordWoodTexture),
        };
    }

    public void Unload()
    {
        Raylib.UnloadTexture(PickaxeWoodTexture);
        Raylib.UnloadTexture(SwordWoodTexture);
    }
}