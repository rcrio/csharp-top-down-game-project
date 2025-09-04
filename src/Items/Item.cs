// removed get id THANKS MALTESER

using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

public abstract class Item
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int MaxStack { get; private set; }
    public Texture2D Texture { get; private set; }
    public bool CanBeInInventory { get; set; }

    public Item(string id, string name, string description, int maxStack, Texture2D texture, bool canBeInInventory = true)
    {
        Id = id;
        Name = name;
        Description = description;
        MaxStack = maxStack;
        Texture = texture;
        CanBeInInventory = canBeInInventory;
    }

    public virtual void Draw(Vector2 drawPosition)
    {
        if (Texture.Id == 0)
        {
            Raylib.DrawRectangle((int)drawPosition.X, (int)drawPosition.Y, 16, 16, new Color(200, 200, 230, 255));
        }
        else
        {
            Raylib.DrawTexture(Texture, (int)drawPosition.X, (int)drawPosition.Y, Color.White);
        }
    }

    public virtual void Draw(int x, int y)
    {
        Vector2 drawPosition = new Vector2(x, y);
        Draw(drawPosition);
    }
    
    public virtual void DrawInSlot(Vector2 drawPosition, int slotSize)
    {
        if (Texture.Id == 0)
        {
            Raylib.DrawRectangle((int)drawPosition.X, (int)drawPosition.Y, slotSize, slotSize, new Color(200, 200, 230, 255));
        }
        else
        {
            Rectangle src = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Rectangle dest = new Rectangle(drawPosition.X, drawPosition.Y, slotSize, slotSize);
            Vector2 origin = Vector2.Zero;

            Raylib.DrawTexturePro(Texture, src, dest, origin, 0f, Color.White);
        }
    }
}