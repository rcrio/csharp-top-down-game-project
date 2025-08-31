using System.Numerics;

public class DroppedItem
{
    public ItemStack ItemStack { get; private set; }
    public Vector2 Position { get; private set; }
    public float PickupRange { get; private set; } = 16f;
    public float LifeTime { get; private set; } = 60f;
    public float TimeLived { get; private set; } = 0f;
    public bool IsAlive { get; private set; } = true;
    public float ThrowDistance { get; private set; }
    public float TravelledDistance { get; set; } = 0;
    public Direction DirectionToThrow { get; private set; }
    public DroppedItem(ItemStack itemStack, Vector2 position, int throwDistance = 0, Direction directionToThrow = Direction.None)
    {
        Position = position;
        ItemStack = itemStack;
        ThrowDistance = throwDistance;
        DirectionToThrow = directionToThrow;
    }

    public void Update(float deltaTime)
    {
        TimeLived += deltaTime;
        if (TimeLived >= LifeTime)
        {
            IsAlive = false;
        }
        Console.WriteLine(TravelledDistance + " " + ThrowDistance);
        if (TravelledDistance < ThrowDistance)
        {
            Console.WriteLine("Distance called");
            var posX = Position.X;
            var posY = Position.Y;

            TravelledDistance += 1;
            if (DirectionToThrow == Direction.North) posY -= 1;
            if (DirectionToThrow == Direction.South) posY += 1;
            if (DirectionToThrow == Direction.West) posX -= 1;
            if (DirectionToThrow == Direction.East) posX += 1;

            Vector2 newVec = new Vector2(posX, posY);
            Position = newVec;
        }
    }

    public void Draw()
    {
        
        ItemStack.Item.Draw((int)Position.X, (int)Position.Y);
    }
}