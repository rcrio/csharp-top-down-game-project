using System.Numerics;
using Raylib_cs;

public abstract class Entity
{
    public Vector2 Position { get; set; }
    public InputManager InputManager { get; set; }
    public GameTime GameTime { get; set; }
    public World World { get; set; }
    public Texture2D Sprite { get; set; }

    // Constructor
    protected Entity(Vector2 position, InputManager inputManager, GameTime gameTime, World world, Texture2D sprite)
    {
        Position = position;
        InputManager = inputManager;
        GameTime = gameTime;
        World = world;
        Sprite = sprite;
    }

    public abstract void Update();
    public abstract void Draw();
    public abstract void Unload();
}
