using System.Numerics;
using Raylib_cs;

public abstract class Entity
{
    public Vector2 Position { get; set; }
    public Texture2D Texture { get; set; }
    public string TexturePath { get; set; }

    // Constructor
    protected Entity(Vector2 position, string texturePath)
    {
        TexturePath = texturePath;
        Position = position;
    }

    public abstract void Update();
    public virtual void Draw()
    {
        if (Texture.Id == 0)
        {
            Raylib.DrawRectangle((int)Position.X, (int)Position.Y, 16, 16, new Color(200, 200, 230, 255));
        }
        else
        {
            Raylib.DrawTexture(Texture, (int)Position.X, (int)Position.Y, Color.White);
        }
    }

    public virtual void Draw(int x, int y)
    {
        if (Texture.Id == 0)
        {
            Raylib.DrawRectangle(x, y, 16, 16, new Color(200, 200, 230, 255));
        }
        else
        {
            Raylib.DrawTexture(Texture, x, y, Color.White);
        }
    }

    // Load
    public virtual void Load()
    {
        AssetManager.LoadTexture16(TexturePath);
    }
    public virtual void Unload()
    {
        if (Texture.Id != 0)
        {
            Raylib.UnloadTexture(Texture);
            Texture = default; // reset
        }
    }
}
