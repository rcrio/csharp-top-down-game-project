using System.Numerics;
using Raylib_cs;

public abstract class Player : Entity
{
    public float Speed { get; private set; } = 250f;
    public int Width { get; private set; } = 16;
    public int Height { get; private set; } = 16;

    // Constructor
    protected Player(Vector2 position, InputManager inputManager, GameTime gameTime, World world, Texture2D sprite)
        : base(position, inputManager, gameTime, world, sprite)
    {

    }

    // Movement helper available to subclasses
    // Document further
    protected void Move(Vector2 direction, float deltaTime)
    {
        if (direction != Vector2.Zero)
        {
            direction = Vector2.Normalize(direction);

            // Start with current position
            Vector2 newPos = Position;

            // Separate movement per axis for smooth sliding
            Vector2 newPosX = new Vector2(Position.X + direction.X * Speed * deltaTime, Position.Y);
            if (World.IsPositionWalkable(newPosX, Width, Height))
                newPos.X = newPosX.X;

            Vector2 newPosY = new Vector2(newPos.X, Position.Y + direction.Y * Speed * deltaTime);
            if (World.IsPositionWalkable(newPosY, Width, Height))
                newPos.Y = newPosY.Y;

            // Apply the final position
            Position = newPos;
        }
    }
}