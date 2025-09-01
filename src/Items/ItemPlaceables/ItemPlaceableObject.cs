using Raylib_cs;

public class ItemPlaceableObject : ItemPlaceable
{
    public ItemPlaceableObject(string id, string name, string description, int maxStack, string spritePath = null) : base(id, name, description, maxStack, spritePath)
    {
        IsWalkable = false;
    }

    public override void Draw(int baseX, int baseY)
    {
        if (Texture.Id == 0)
        {
            Raylib.DrawRectangle(baseX, baseY, Constants.TILE_SIZE, Constants.TILE_SIZE, new Color(200,200,230,255));
            return;
        }

        int width = Texture.Width;
        int height = Texture.Height;

        // Anchor bottom-center, shift down **two tiles**
        int drawX = baseX + Constants.TILE_SIZE / 2 - width / 2;
        int drawY = baseY + (Constants.TILE_SIZE * 2) - height; // <-- shift down two tiles

        drawY -= 0; // adjust this value for visual preference

        Raylib.DrawTexture(Texture, drawX, drawY, Color.White);
    }


}