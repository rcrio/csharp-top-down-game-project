using Raylib_cs;

public class ItemPlaceableTerrain : ItemPlaceable
{
    public ItemPlaceableTerrain(string id, string name, string description, int maxStack, string spritePath = null) : base(id, name, description, maxStack, spritePath)
    {
        IsWalkable = true;
        CanBeInInventory = false;
    }

    public override void Draw(int x, int y)
    {
        if (Sprite.Id == 0)
        {
            Raylib.DrawRectangle(x, y, 16, 16, new Color(200, 200, 200, 255));
        }
        else
        {
            
        }
    }
}