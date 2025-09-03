using Raylib_cs;

public class ItemPlaceableFloor : ItemPlaceable
{
    public ItemPlaceableFloor(string id, string name, string description, int maxStack, Texture2D texture) : base(id, name, description, maxStack, texture)
    {
        IsWalkable = true;
    }
}