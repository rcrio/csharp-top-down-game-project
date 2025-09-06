using System.Numerics;
using Raylib_cs;

public class LocalPlayer : Player
{
    private InputManager _inputManager;
    private FactoryLoader _factoryLoader;
    private TileSelector _tileSelector;
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
        FactoryLoader factoryLoader,
        TileSelector tileSelector,
        float speed = 100f,
        float pickUpRange = 32f
        )
        :
        base(position, northTexturePath, southTexturePack, westTexturePath, eastTexturePath, gameTime, world, inventorySize, speed, pickUpRange)
    {
        _inputManager = inputManager;
        _factoryLoader = factoryLoader;
        _tileSelector = tileSelector;
    }

    // Refactor eventually to make player's face the cursor.
    public void Update(float deltaTime)
    {
        // Drop selected item logic
        if (_inputManager.DropItem() && Inventory.ItemStacks[Inventory.currentSelectedIndex] != null)
        {
            Console.WriteLine("Item drop called.");

            var itemToDrop = Inventory.ItemStacks[Inventory.currentSelectedIndex];
            var itemToDropClone = itemToDrop.Clone(1);
            World.DroppedItemManager.AddThrownDroppedItem(itemToDropClone, Position, FacingDirection);
            Inventory.RemoveItem(itemToDrop.Item, 1);

        }

        // Pick up item logic
        _timeSinceLastPickup += GameTime.DeltaTime;

        if (_timeSinceLastPickup >= _pickupCooldown)
        {
            foreach (DroppedItem itemToCollect in World.DroppedItemManager.GetDroppedItemsInRadius(PickupBounds))
            {
                int quantityToCollect = itemToCollect.ItemStack.Quantity;
                if (Inventory.AddItem(itemToCollect.ItemStack.Item, quantityToCollect))
                {
                    World.DroppedItemManager.RemoveDroppedItem(itemToCollect);

                    _timeSinceLastPickup = 0f; // reset cooldown
                    break; // optional: only pick up one item per cooldown
                }
            }
        }

        // Movement logic
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

        // Change second to a method in inventory to return current selected item
        if (_inputManager.LeftClick() && Inventory.GetSelectedItemStack() != null)
        {
            var selectedItem = Inventory.GetSelectedItemStack().Item;
            if (selectedItem is ItemPlaceable placeable && _tileSelector.Tile != null)
            {
                // You can now use 'placeable' as an ItemPlaceable
                if (selectedItem.Use(_tileSelector.Tile))
                {
                    Inventory.GetSelectedItemStack().Remove(1);
                }
            }
        }

        if (_inputManager.LeftClick() && Inventory.GetSelectedItemStack() == null)
        {
            _tileSelector.BreakFloor();
        }
    }

    public void GenerateDefaultInventory()
    {
        Inventory.AddItemByIndex(0, _factoryLoader.ItemFactory.Items["wood_sword"]);
        Inventory.AddItemByIndex(1, _factoryLoader.ItemFactory.Items["wood_pickaxe"]);
        Inventory.AddItemByIndex(2, _factoryLoader.FloorFactory.Floors["wood"], 98);
        Inventory.AddItemByIndex(3, _factoryLoader.FloorFactory.Floors["wood"], 30);
        Inventory.AddItemByIndex(4, _factoryLoader.FloorFactory.Floors["wood"], 30);
        Inventory.AddItemByIndex(5, _factoryLoader.WallFactory.Walls["wood"], 30);
    }

    // Refactor, move graphics out of player into local player.
    public virtual void Load(World world)
    {
        World = world;
        GenerateDefaultInventory();
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

}