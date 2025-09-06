using Raylib_cs;

public class ItemPlaceableFloor : ItemPlaceable
{
    public ItemPlaceableFloor(string id, string name, string description, int maxStack, Texture2D texture) : base(id, name, description, maxStack, texture)
    {
        IsWalkable = true;
    }

    public override bool Use(Tile tile)
    {
        if (tile._itemPlaceableFloor == null)
        {
            tile._itemPlaceableFloor = this;
            return true;
        }
        return false;
    }
}