using System.Numerics;
using System.Runtime;
using Raylib_cs;

public abstract class Entity
{
    public Vector2 Position { get; set; }
    public Texture2D Texture { get; set; }
    public string texturePath { get; set; }

    // Constructor
    protected Entity(Vector2 position, string texturePath)
    {
        Position = position;
    }

    public abstract void Update();
    public abstract void Draw();
    // Load
    public virtual void Load()
    {
        
    }
    public virtual void Unload()
    {
        
    }
}
