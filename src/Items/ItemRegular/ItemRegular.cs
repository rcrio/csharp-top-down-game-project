using Raylib_cs;

public class ItemRegular : Item
{
    public ItemRegular(string id, string name, string description, int maxStack, Texture2D texture) : base(id, name, description, maxStack, texture) { } 

    public override void Use(World world, Player player)
    {
        // Regular items have no use effect by default
    }
}