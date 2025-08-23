using System.Numerics;
using Raylib_cs;

public abstract class Item
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int MaxStack { get; private set; }
    public string SpritePath { get; set; }
    public Texture2D Sprite { get; private set; }
    public bool CanBeInInventory { get; set; }

    public Item(string id, string name, string description, int maxStack, string spritePath = null, bool canBeInInventory = true)
    {
        Id = id;
        Name = name;
        Description = description;
        MaxStack = maxStack;
        SpritePath = spritePath;
        CanBeInInventory = canBeInInventory;

        if (!string.IsNullOrEmpty(spritePath))
        {
            Sprite = AssetManager.LoadTexture(spritePath);
        }
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
            Raylib.DrawTexture(Sprite, (int)drawPosition.X, (int)drawPosition.Y, Color.White);
        }
    }

    public virtual void DrawInSlot(Vector2 drawPosition, int slotSize)
    {
        if (Sprite.Id == 0)
        {
            Raylib.DrawRectangle((int)drawPosition.X, (int)drawPosition.Y, slotSize, slotSize, new Color(200, 200, 230, 255));
        }
        else
        {
            Rectangle src = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
            Rectangle dest = new Rectangle(drawPosition.X, drawPosition.Y, slotSize, slotSize);
            Vector2 origin = Vector2.Zero;

            Raylib.DrawTexturePro(Sprite, src, dest, origin, 0f, Color.White);
        }
    }

    public virtual void Unload()
    {
        if (Sprite.Id != 0)
        {
            Raylib.UnloadTexture(Sprite);
            Sprite = default; // reset
        }
    }
}