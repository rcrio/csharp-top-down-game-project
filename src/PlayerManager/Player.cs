using System.Numerics;
using Raylib_cs;

// Could remove inheritance from Entity, we're overriding a lot of the methods
public abstract class Player
{
    public Vector2 Position { get; set; }
    public Texture2D NorthTexture { get; set; }
    public Texture2D SouthTexture { get; set; }
    public Texture2D WestTexture { get; set; }
    public Texture2D EastTexture { get; set; }
    public string NorthTexturePath { get; set; }
    public string SouthTexturePath { get; set; }
    public string WestTexturePath { get; set; }
    public string EastTexturePath { get; set; }
    public int Width { get; private set; } = 16;
    public int Height { get; private set; } = 16;
    public Vector2 Center => new Vector2(Position.X + Width / 2, Position.Y + Height / 2);

    // position, texture inherited
    public GameTime GameTime { get; private set; }
    public World World { get; private set; }
    public Inventory Inventory { get; private set; }

    public float Speed { get; private set; }
    public float PickupRange { get; private set; }
    public Direction FacingDirection { get; set; } = Direction.South;
    protected float _pickupCooldown = 0.09f; // seconds between pickups
    protected float _timeSinceLastPickup = 0f;


    protected Player(
        Vector2 position,
        string northTexturePath,
        string southTexturePack,
        string westTexturePath,
        string eastTexturePath,
        GameTime gameTime,
        World world,
        int inventorySize,
        float speed = 200f,
        float pickUpRange = 32f)
    {
        NorthTexturePath = northTexturePath;
        SouthTexturePath = southTexturePack;
        WestTexturePath = westTexturePath;
        EastTexturePath = eastTexturePath;
        Position = position;
        GameTime = gameTime;
        World = world;
        Inventory = new Inventory(inventorySize);
        Speed = speed;
        PickupRange = pickUpRange;
        Load();
    }

    // Refactor, move graphics out of player into local player.
    public virtual void Load()
    {
        Console.WriteLine("Loading player textures...");
        NorthTexture = AssetManager.LoadTexture(NorthTexturePath);
        SouthTexture = AssetManager.LoadTexture(SouthTexturePath);
        WestTexture = AssetManager.LoadTexture(WestTexturePath);
        EastTexture = AssetManager.LoadTexture(EastTexturePath);
    }

    public virtual void Unload()
    {
        if (NorthTexture.Id != 0)
        {
            Raylib.UnloadTexture(NorthTexture);
            NorthTexture = default; // reset
        }
        if (SouthTexture.Id != 0)
        {
            Raylib.UnloadTexture(SouthTexture);
            SouthTexture = default; // reset
        }
        if (WestTexture.Id != 0)
        {
            Raylib.UnloadTexture(WestTexture);
            WestTexture = default; // reset
        }
        if (EastTexture.Id != 0)
        {
            Raylib.UnloadTexture(EastTexture);
            EastTexture = default; // reset
        }
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

    public void Draw()
    {
        if (NorthTexture.Id == 0)
        {
            Raylib.DrawRectangle((int)Position.X, (int)Position.Y, 16, 16, new Color(200, 200, 230, 255));
        }
        else
        {
            if (Direction.North == FacingDirection)
            {
                Raylib.DrawTexture(NorthTexture, (int)Position.X, (int)Position.Y, Color.White);
            }
            else if (Direction.South == FacingDirection)
            {
                Raylib.DrawTexture(SouthTexture, (int)Position.X, (int)Position.Y, Color.White);
            }
            else if (Direction.West == FacingDirection)
            {
                Raylib.DrawTexture(WestTexture, (int)Position.X, (int)Position.Y, Color.White);
            }
            else if (Direction.East == FacingDirection)
            {
                Raylib.DrawTexture(EastTexture, (int)Position.X, (int)Position.Y, Color.White);
            }
        }
    }

    public Rectangle PickupBounds
    {
        get
        {
            return new Rectangle(
                // Get the co-ordinate the top-left as that's where rectangle starts drawing
                Position.X - PickupRange,
                Position.Y - PickupRange,
                Width + PickupRange * 2,
                Height + PickupRange * 2
            );
        }
    }

}
