using Raylib_cs;

public abstract class ItemPlaceable : Item
{
    public bool IsWalkable { get; set; }
    public ItemPlaceable(string id, string name, string description, int maxStack, string spritePath = null) : base(id, name, description, maxStack, spritePath) { }

    public override void Draw(int baseX, int baseY)
    {
        if (Texture.Id == 0) return; // no texture loaded

        int width = Texture.Width;
        int height = Texture.Height;

        // Anchor bottom-center of sprite to base tile
        int drawX = baseX + Constants.TILE_SIZE / 2 - width / 2;
        int drawY = baseY - (height - Constants.TILE_SIZE);

        Raylib.DrawTexture(Texture, drawX, drawY, Color.White);
    }
}

    