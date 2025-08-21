using Raylib_cs;

public class ItemPlaceableObject : ItemPlaceable
{
    public ItemPlaceableObject(string name, string description, Texture2D sprite) : base(name, description, sprite)
    {
        IsWalkable = false;
    }

    public override void Draw(int x, int y)
    {
        if (Sprite.Id == 0)
        {
            Raylib.DrawRectangle(x, y, 16, 16, new Color(200, 255, 200, 255));
        }
        else
        {
            
        }
    }
}