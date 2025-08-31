using System.Numerics;

public class DroppedItem
{
    public Vector2 Position { get; set; }
    public ItemStack ItemStack { get; set; }
    public float PickupRange { get; set; } = 16f;
    public float LifeTime { get; set; } = 60f;
    public float TimeLived { get; set; } = 0f;
    public DroppedItem(Vector2 position, ItemStack itemStack)
    {
        Position = position;
        ItemStack = itemStack;
    }

    public void Update(float deltaTime)
    {
        TimeLived += deltaTime;
        if (TimeLived >= LifeTime)
        {
            // Handle item expiration (e.g., remove it from the world)
        }
    }

    public void Draw(int x, int y)
    {
        ItemStack.Item.Draw(x, y);
    }
}