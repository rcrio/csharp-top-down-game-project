using System.Numerics;
using Raylib_cs;

public class CustomPlayer1 : Player
{
    public float Speed { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Vector2 Center { get; private set; }

    public Inventory Inventory { get; private set; }

    // Constructor
    public CustomPlayer1(Vector2 position, InputManager inputManager, GameTime gameTime, World world, Texture2D sprite)
        : base(position, inputManager, gameTime, world, sprite)
    {
        Speed = 250f;
        Width = 16;
        Height = 16;
        Center = new Vector2(Width / 2, Height / 2);
        Inventory = new Inventory(50);
        GenerateDefaultInventory();
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

    public void GenerateDefaultInventory()
    {
        Inventory.AddItemByIndex(0, ItemFactory.Items["item_regular_sword"]);
    }
    public override void Unload()
    {

    }
}