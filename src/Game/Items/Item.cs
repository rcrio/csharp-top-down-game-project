using System.Numerics;
using Raylib_cs;

public abstract class Item
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int MaxStack { get; private set; }
    public Texture2D Sprite { get; private set; }
    public bool CanBeInInventory { get; set; }

    public Item(string id, string name, string description, int maxStack, Texture2D sprite = default, bool canBeInInventory = true)
    {
        Id = id;
        Name = name;
        Description = description;
        MaxStack = maxStack;
        Sprite = sprite;
        CanBeInInventory = canBeInInventory;
    }

    public virtual string GetId()
    {
        return Id;
    }

    public virtual void Draw(Vector2 drawPosition)
    {
        if (Sprite.Id == 0)
        {
            Raylib.DrawRectangle((int)drawPosition.X, (int)drawPosition.Y, 16, 16, new Color(200, 200, 230, 255));
        }
        else
        {
            
        }
    }
}