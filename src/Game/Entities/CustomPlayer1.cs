using System.Numerics;
using Raylib_cs;

public class CustomPlayer1 : Player
{
    public float Speed { get; private set; } = 250f;
    public int Width { get; private set; } = 16;
    public int Height { get; private set; } = 16;

    private Inventory _inventory;

    // Constructor
    public CustomPlayer1(Vector2 position, InputManager inputManager, GameTime gameTime, World world, Texture2D sprite)
        : base(position, inputManager, gameTime, world, sprite)
    {
        _inventory = new Inventory(40);
    }

    public override void Update()
    {
        Vector2 input = Vector2.Zero;

        if (InputManager.IsActionDown(Action.MoveUp)) input.Y -= 1;
        if (InputManager.IsActionDown(Action.MoveDown)) input.Y += 1;
        if (InputManager.IsActionDown(Action.MoveLeft)) input.X -= 1;
        if (InputManager.IsActionDown(Action.MoveRight)) input.X += 1;

        Move(input, GameTime.DeltaTime);
    }

    public override void Draw()
    {
        // Draw a green 16x16 rectangle at the player's position
        Raylib.DrawRectangle(
            (int)Position.X,
            (int)Position.Y,
            Width,
            Height,
            Color.Green
        );
    }

    public override void Unload()
    {
        
    }
}