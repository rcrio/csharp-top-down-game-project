using System.Numerics;
using Raylib_cs;

public abstract class Entity
{
    public Vector2 Position { get; set; }
    public Texture2D Sprite { get; set; }

    // Constructor
    protected Entity(Vector2 position, Texture2D sprite)
    {
        Position = position;
        Sprite = sprite;
    }

    public abstract void Update();
    public abstract void Draw();
    public abstract void Unload();
}
