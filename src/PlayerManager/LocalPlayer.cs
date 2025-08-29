using System.Numerics;
using Raylib_cs;

public class LocalPlayer : Player
{
    private InputManager _inputManager;
    public LocalPlayer(
        Vector2 position,
        string northTexturePath,
        string southTexturePack,
        string westTexturePath,
        string eastTexturePath,
        GameTime gameTime,
        World world,
        int inventorySize,
        InputManager inputManager,
        float speed = 200f,
        float pickUpRange = 32f)
        : base(position, northTexturePath, southTexturePack, westTexturePath, eastTexturePath, gameTime, world, inventorySize, speed, pickUpRange)
    {
        _inputManager = inputManager;
        GenerateDefaultInventory();
    }

    public void Update()
    {
        Vector2 input = Vector2.Zero;

        if (_inputManager.MoveUp())
        {
            input.Y -= 1;
            FacingDirection = Direction.North;
        }
        if (_inputManager.MoveDown())
        {
            input.Y += 1;
            FacingDirection = Direction.South;
        }
        if (_inputManager.MoveLeft())
        {
            input.X -= 1;
            FacingDirection = Direction.West;
        }
        if (_inputManager.MoveRight())
        {
            input.X += 1;
            FacingDirection = Direction.East;
        }

        Move(input, GameTime.DeltaTime);
    }

    public void GenerateDefaultInventory()
    {
        Inventory.AddItemByIndex(0, ItemFactory.Items["item_regular_wooden_pickaxe"]);
        Inventory.AddItemByIndex(1, ItemFactory.Items["item_regular_wooden_sword"]);
        Inventory.AddItemByIndex(2, FloorFactory.Floors["floor_wood"], 98);
        Inventory.AddItemByIndex(3, FloorFactory.Floors["floor_wood"], 30);
        Inventory.AddItemByIndex(4, FloorFactory.Floors["floor_wood"], 30);
    }
}