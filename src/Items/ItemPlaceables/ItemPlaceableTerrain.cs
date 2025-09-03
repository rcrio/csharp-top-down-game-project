using Raylib_cs;

public class ItemPlaceableTerrain : ItemPlaceable
{
    public ItemPlaceableTerrain(string id, string name, string description, int maxStack, Texture2D texture) : base(id, name, description, maxStack, texture)
    {
        IsWalkable = true;
        CanBeInInventory = false;
    }
}