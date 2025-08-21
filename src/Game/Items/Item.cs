using Raylib_cs;

public abstract class Item
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Texture2D Sprite { get; set; }
    public bool CanBeInInventory { get; protected set; }

    public Item(string name, string description, Texture2D sprite = default, bool canBeInInventory = true)
    {
        Name = name;
        Description = description;
        Sprite = sprite;
        CanBeInInventory = canBeInInventory;
    }
}