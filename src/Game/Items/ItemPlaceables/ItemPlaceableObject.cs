using Raylib_cs;

public class ItemPlaceableObject : ItemPlaceable
{
    public ItemPlaceableObject(string id, string name, string description, int maxStack, string spritePath = null) : base(id, name, description, maxStack, spritePath)
    {
        IsWalkable = false;
    }
}