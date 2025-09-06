using System.Numerics;
using Raylib_cs;

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

    // Sprite size
    public int Width { get; private set; } = 16;
    public int Height { get; private set; } = 16;

    // Collision box size (slightly smaller to prevent jank collisions)
    private const int CollisionWidth = 12;
    private const int CollisionHeight = 12;

    public Vector2 Center => new Vector2(Position.X + Width / 2, Position.Y + Height / 2);

    public GameTime GameTime { get; private set; }
    public World World { get; protected set; }
    public Inventory Inventory { get; private set; }

    public float Speed { get; private set; }
    public float PickupRange { get; private set; }
    public Direction FacingDirection { get; set; } = Direction.South;
    protected float _pickupCooldown = 0.09f;
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
    }

    // Build a smaller collision box centered inside the sprite
    private Rectangle GetCollisionBox(Vector2 pos)
    {
        int padX = (Width - CollisionWidth) / 2;
        int padY = (Height - CollisionHeight) / 2;

        return new Rectangle(
            pos.X + padX,
            pos.Y + padY,
            CollisionWidth,
            CollisionHeight
        );
    }

    protected void Move(Vector2 direction, float deltaTime)
    {
        if (direction == Vector2.Zero) return;

        direction = Vector2.Normalize(direction);
        Vector2 deltaMove = direction * Speed * deltaTime;
        Vector2 newPos = Position;

        // Horizontal
        if (deltaMove.X != 0)
        {
            Rectangle testBoxX = GetCollisionBox(new Vector2(newPos.X + deltaMove.X, newPos.Y));
            if (World.IsBoxWalkable(testBoxX))
                newPos.X += deltaMove.X;
        }

        // Vertical
        if (deltaMove.Y != 0)
        {
            Rectangle testBoxY = GetCollisionBox(new Vector2(newPos.X, newPos.Y + deltaMove.Y));
            if (World.IsBoxWalkable(testBoxY))
                newPos.Y += deltaMove.Y;
        }

        Position = newPos;
    }

    public void Draw()
    {
        if (NorthTexture.Id == 0)
        {
            Raylib.DrawRectangle((int)Position.X, (int)Position.Y, Width, Height, new Color(200, 200, 230, 255));
        }
        else
        {
            if (Direction.North == FacingDirection)
                Raylib.DrawTexture(NorthTexture, (int)Position.X, (int)Position.Y, Color.White);
            else if (Direction.South == FacingDirection)
                Raylib.DrawTexture(SouthTexture, (int)Position.X, (int)Position.Y, Color.White);
            else if (Direction.West == FacingDirection)
                Raylib.DrawTexture(WestTexture, (int)Position.X, (int)Position.Y, Color.White);
            else if (Direction.East == FacingDirection)
                Raylib.DrawTexture(EastTexture, (int)Position.X, (int)Position.Y, Color.White);
        }

        // Debug: draw collision box in red
        Rectangle colBox = GetCollisionBox(Position);
        Raylib.DrawRectangleLines((int)colBox.X, (int)colBox.Y, (int)colBox.Width, (int)colBox.Height, Color.Red);
    }

    public Rectangle PickupBounds
    {
        get
        {
            return new Rectangle(
                Position.X - PickupRange,
                Position.Y - PickupRange,
                Width + PickupRange * 2,
                Height + PickupRange * 2
            );
        }
    }
}
