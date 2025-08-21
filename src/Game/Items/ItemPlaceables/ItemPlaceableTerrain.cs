using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

public class ItemPlaceableTerrain : ItemPlaceable
{
    public ItemPlaceableTerrain(string name, string description, Texture2D sprite) : base(name, description, sprite)
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