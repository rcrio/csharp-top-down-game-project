
using System.Numerics;
using Raylib_cs;

public abstract class ItemPlaceable : Item
{
    public bool IsWalkable { get; set; }
    public ItemPlaceable(string id, string name, string description, int maxStack, string spritePath = null) : base(id, name, description, maxStack, spritePath) { } 

    // scuffed, needs refactoring
    public virtual void Draw(int x, int y)
    {
        Vector2 drawPosition = new Vector2(x, y);
        if (Sprite.Id == 0)
        {
            Raylib.DrawRectangle((int)drawPosition.X, (int)drawPosition.Y, 16, 16, new Color(200, 200, 230, 255));
        }
        else
        {
            Raylib.DrawTexture(Sprite, (int)drawPosition.X, (int)drawPosition.Y, Color.White);
        }
    }
}