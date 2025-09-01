using System.Numerics;
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
            Raylib.DrawRectangle(baseX, baseY, Constants.TILE_SIZE, Constants.TILE_SIZE, new Color(200, 200, 230, 255));
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

    /*
    public void DrawWallWithPlayerOverlap(int baseX, int baseY)
    {
        if (Texture.Id == 0)
        {
            Raylib.DrawRectangle(baseX, baseY, Constants.TILE_SIZE, Constants.TILE_SIZE, new Color(200, 200, 230, 255));
            return;
        }

        int width = Texture.Width;
        int height = Texture.Height;
        int overlap = 4; // top pixels behind player

        // Draw bottom part (in front of player)
        Rectangle bottomPart = new Rectangle(0, overlap, width, height - overlap);
        Raylib.DrawTextureRec(Texture, bottomPart, new Vector2(baseX + Constants.TILE_SIZE / 2 - width / 2, baseY + Constants.TILE_SIZE - (height - overlap)), Color.White);

        // Draw top part (behind player)
        Rectangle topPart = new Rectangle(0, 0, width, overlap);
        Raylib.DrawTextureRec(Texture, topPart, new Vector2(baseX + Constants.TILE_SIZE / 2 - width / 2, baseY + Constants.TILE_SIZE - height), Color.White);
    } */
    
    // Draw the **top pixels behind the player**
    public void DrawWallTopBehindPlayer(int baseX, int baseY)
{
    if (Texture.Id == 0) return;

    int width = Texture.Width;
    int height = Texture.Height;
    int topPixelsBehindPlayer = 4;

    // EXACTLY same draw position as your original Draw
    int drawX = baseX + Constants.TILE_SIZE / 2 - width / 2;
    int drawY = baseY + (Constants.TILE_SIZE * 2) - height;

    drawY -= 4;

    // Draw only the top pixels behind the player
        Rectangle topPart = new Rectangle(0, 0, width, topPixelsBehindPlayer);
    Raylib.DrawTextureRec(Texture, topPart, new Vector2(drawX, drawY), Color.White);
}

public void DrawWallBottomInFront(int baseX, int baseY)
{
    if (Texture.Id == 0)
    {
        Raylib.DrawRectangle(baseX, baseY, Constants.TILE_SIZE, Constants.TILE_SIZE, new Color(200, 200, 230, 255));
        return;
    }

    int width = Texture.Width;
    int height = Texture.Height;
    int topPixelsBehindPlayer = 4;

    // EXACTLY same draw position as your original Draw
    int drawX = baseX + Constants.TILE_SIZE / 2 - width / 2;
    int drawY = baseY + (Constants.TILE_SIZE * 2) - height;

    drawY -= 4;

    // Draw the bottom part in front of the player
    Rectangle bottomPart = new Rectangle(0, topPixelsBehindPlayer, width, height - topPixelsBehindPlayer);
    Raylib.DrawTextureRec(Texture, bottomPart, new Vector2(drawX, drawY + topPixelsBehindPlayer), Color.White);
}




}