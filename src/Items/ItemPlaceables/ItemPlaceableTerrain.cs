public class ItemPlaceableTerrain : ItemPlaceable
{
    public ItemPlaceableTerrain(string id, string name, string description, int maxStack, string spritePath = null) : base(id, name, description, maxStack, spritePath)
    {
        IsWalkable = true;
        CanBeInInventory = false;
    }
}