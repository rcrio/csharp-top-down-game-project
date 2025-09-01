using System.Numerics;

public class LocalPlayer : Player
{
    private InputManager _inputManager;

    // Sounds
    private SoundPool _itemCollectSoundPool;
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
        float pickUpRange = 32f
        )
        :
        base(position, northTexturePath, southTexturePack, westTexturePath, eastTexturePath, gameTime, world, inventorySize, speed, pickUpRange)
    {
        _inputManager = inputManager;
        GenerateDefaultInventory();

        _itemCollectSoundPool = new SoundPool("item_collect.mp3", 8);
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
                    _itemCollectSoundPool.Play();

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