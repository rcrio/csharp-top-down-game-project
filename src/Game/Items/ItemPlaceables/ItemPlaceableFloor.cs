using Raylib_cs;

public class ItemPlaceableFloor : ItemPlaceable
{
    public ItemPlaceableFloor(string name, string description, Texture2D sprite) : base(name, description, sprite)
    {
        IsWalkable = true;
    }

    public override void Draw(int x, int y)
    {
        if (Sprite.Id == 0)
        {
            Raylib.DrawRectangle(x, y, 16, 16, new Color(200, 200, 230, 255));
        }
        else
        {
            
        }
    }
}