using System.Numerics;
using Raylib_cs;

public class ItemPlaceableObject : ItemPlaceable
{
    public ItemPlaceableObject(string id, string name, string description, int maxStack, Texture2D texture) : base(id, name, description, maxStack, texture)
    {
        IsWalkable = false;
    }
    /*
    public override void Draw(int baseX, int baseY)
    {
        if (Texture.Id == 0)
        {
            Raylib.DrawRectangle(baseX, baseY, Constants.TILE_SIZE, Constants.TILE_SIZE, new Color(200, 200, 230, 255));
            return;
        }

        int width = Texture.Width;
        int height = Texture.Height;

        // Anchor bottom-center to the tile
        int drawX = baseX + Constants.TILE_SIZE / 2 - width / 2;
        int drawY = baseY + Constants.TILE_SIZE - height; // base of sprite aligns with tile

        Raylib.DrawTexture(Texture, drawX, drawY, Color.White);
    }*/

    public void DrawWallTopBehindPlayer(int baseX, int baseY)
    {
        if (Texture.Id == 0) return;

        int width = Texture.Width;
        int height = Texture.Height;
        int topPixelsBehindPlayer = 10;

        int drawX = baseX + Constants.TILE_SIZE / 2 - width / 2;
        int drawY = baseY + (Constants.TILE_SIZE * 2) - height;

        drawY -= Constants.TILE_SIZE - 2; // shift 1 tile up

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
        int topPixelsBehindPlayer = 10;

        int drawX = baseX + Constants.TILE_SIZE / 2 - width / 2;
        int drawY = baseY + (Constants.TILE_SIZE * 2) - height;

        drawY -= Constants.TILE_SIZE; // shift 1 tile up

        Rectangle bottomPart = new Rectangle(0, topPixelsBehindPlayer, width, height - topPixelsBehindPlayer);
        Raylib.DrawTextureRec(Texture, bottomPart, new Vector2(drawX, drawY + topPixelsBehindPlayer), Color.White);
    }

    public override bool Use(Tile tile)
    {
        if (tile._itemPlaceableObject == null)
        {
            tile._itemPlaceableObject = this;
            return true;
        }
        return false;
    }
}