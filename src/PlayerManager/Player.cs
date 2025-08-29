using System.Numerics;
using Raylib_cs;

public abstract class Player : Entity
{
    public int Width { get; private set; } = 16;
    public int Height { get; private set; } = 16;
    public Vector2 Center => new Vector2(Position.X + Width / 2, Position.Y + Height / 2);

    // position, texture inherited
    public GameTime GameTime { get; private set; }
    public World World { get; private set; }
    public Inventory Inventory { get; private set; }

    public float Speed { get; private set; }
    public float PickupRange { get; private set; }


    protected Player(Vector2 position, string texturePath, GameTime gameTime, World world, int inventorySize, float speed = 200f, float pickUpRange = 32f)
        : base(position, texturePath)
    {
        GameTime = gameTime;
        World = world;
        Inventory = new Inventory(inventorySize);
        Speed = speed;
        PickupRange = pickUpRange;
    }

    /*
    Why separate rectangles
    testBoxX: rectangle at new X, old Y
    Checks horizontal collisions without worrying about vertical position.
    testBoxY: rectangle at new X, new Y
    Checks vertical collisions with updated horizontal position.
    By checking separately, your player can:
    Move along X if Y is blocked.
    Move along Y if X is blocked.
    Slide diagonally smoothly if no walls block either axis.
    */
    protected void Move(Vector2 direction, float deltaTime)
    {
        // If there is no movement input, exit early
        if (direction == Vector2.Zero) return;

        // Normalize the direction vector so diagonal movement isn't faster
        // Example: moving (1,1) diagonally should not move faster than (1,0) horizontally
        direction = Vector2.Normalize(direction);

        // Calculate how far the player wants to move this frame
        // deltaMove = movement vector scaled by speed and deltaTime
        Vector2 deltaMove = direction * Speed * deltaTime;

        // Start with current position
        Vector2 newPos = Position;

        // --- Horizontal movement ---
        if (deltaMove.X != 0)
        {
            // Create a rectangle representing the player's proposed horizontal position
            // testBoxX covers the player's entire width and height at the new horizontal position
            Rectangle testBoxX = new Rectangle(newPos.X + deltaMove.X, newPos.Y, Width, Height);

            // Check if that rectangle is walkable in the world (no walls/obstacles)
            if (World.IsBoxWalkable(testBoxX))
            {
                // If walkable, update the horizontal position
                newPos.X += deltaMove.X;
            }
            // else blocked, do not move horizontally
        }

        // --- Vertical movement ---
        if (deltaMove.Y != 0)
        {
            // Create a rectangle representing the player's proposed vertical position
            // testBoxY covers the player's entire width and height at the new vertical position
            Rectangle testBoxY = new Rectangle(newPos.X, newPos.Y + deltaMove.Y, Width, Height);

            // Check if that rectangle is walkable in the world (no walls/obstacles)
            if (World.IsBoxWalkable(testBoxY))
            {
                // If walkable, update the vertical position
                newPos.Y += deltaMove.Y;
            }
            // else blocked, do not move vertically
        }

        // Apply the final position after horizontal and vertical movement
        Position = newPos;
    }


}
