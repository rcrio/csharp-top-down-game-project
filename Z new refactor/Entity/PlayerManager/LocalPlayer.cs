public class LocalPlayer : Player
{
    public float Speed { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Vector2 Center { get; private set; }

    public Inventory Inventory { get; private set; }

    // Constructor
    public LocalPlayer(Vector2 position, InputManager inputManager, GameTime gameTime, World world, Texture2D sprite)
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

        if (InputManager.MoveUp()) input.Y -= 1;
        if (InputManager.MoveDown()) input.Y += 1;
        if (InputManager.MoveLeft()) input.X -= 1;
        if (InputManager.MoveRight()) input.X += 1;

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
        Inventory.AddItemByIndex(0, ItemFactory.Items["item_regular_wooden_pickaxe"]);
        Inventory.AddItemByIndex(1, ItemFactory.Items["item_regular_wooden_sword"]);
        Inventory.AddItemByIndex(2, ItemPlaceableFactory.PlaceableItems["floor_wood"], 98);
        Inventory.AddItemByIndex(3, ItemPlaceableFactory.PlaceableItems["floor_wood"], 30);
        Inventory.AddItemByIndex(4, ItemPlaceableFactory.PlaceableItems["floor_wood"], 30);
    }
    public override void Unload()
    {

    }
}