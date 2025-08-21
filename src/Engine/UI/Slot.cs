using Raylib_cs;
using System.Numerics;

// Document Further
public class Slot : UIElement
{
    public ItemStack ItemStack { get; set; } = null;

    public Slot(Vector2 position, Vector2 size) : base(position, size)
    {
    }

    public override void Update()
    {
        // For now, nothing to update. Later we can add click/hover logic.
    }

    public override void Draw()
    {
        // Draw slot background
        Raylib.DrawRectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y, Color.DarkGray);
        Raylib.DrawRectangleLines((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y, Color.Black);

        // Draw item sprite if exists
        if (ItemStack != null && ItemStack.Item.Sprite.Id != 0)
        {
            float scaleX = Size.X / 16f;
            float scaleY = Size.Y / 16f;
            Rectangle source = new Rectangle(0, 0, 16, 16);
            Rectangle dest = new Rectangle(Position.X, Position.Y, 16 * scaleX, 16 * scaleY);
            Raylib.DrawTexturePro(ItemStack.Item.Sprite, source, dest, new Vector2(0, 0), 0, Color.White);

            // Draw quantity if > 1
            if (ItemStack.Quantity > 1)
            {
                Raylib.DrawText(ItemStack.Quantity.ToString(), (int)(Position.X + Size.X - 10), (int)(Position.Y + Size.Y - 12), 12, Color.White);
            }
        }
    }
}
