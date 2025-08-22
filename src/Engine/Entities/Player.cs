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
        if (direction == Vector2.Zero)
            return;

        direction = Vector2.Normalize(direction);
        Vector2 newPos = Position;

        // --- Horizontal movement ---
        if (direction.X != 0)
        {
            float deltaX = direction.X * Speed * deltaTime;
            float stepX = Math.Sign(deltaX); // move 1 pixel at a time in the right direction

            float remainingX = Math.Abs(deltaX);
            while (remainingX > 0)
            {
                // Check 1-pixel step in the direction of movement
                Vector2 testPos = new Vector2(newPos.X + stepX, newPos.Y);
                if (!World.IsPositionWalkable(testPos, Width, Height))
                    break; // hit a wall, stop horizontal movement

                newPos.X += stepX;
                remainingX -= 1;
            }
        }

        // --- Vertical movement ---
        if (direction.Y != 0)
        {
            float deltaY = direction.Y * Speed * deltaTime;
            float stepY = Math.Sign(deltaY);

            float remainingY = Math.Abs(deltaY);
            while (remainingY > 0)
            {
                Vector2 testPos = new Vector2(newPos.X, newPos.Y + stepY);
                if (!World.IsPositionWalkable(testPos, Width, Height))
                    break; // hit a wall, stop vertical movement

                newPos.Y += stepY;
                remainingY -= 1;
            }
        }

        Position = newPos;
    }


}