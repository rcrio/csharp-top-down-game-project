public class ItemStack
{
    public Item Item { get; private set; }
    public int Quantity { get; private set; }
    public int MaxStack { get; private set; }

    public ItemStack(Item item, int quantity = 1, int maxStack = 99)
    {
        Item = item;
        Quantity = quantity;
        MaxStack = maxStack;
    }

    public bool IsFull => Quantity >= MaxStack;

    public int Add(int amount)
    {
        int spaceLeft = MaxStack - Quantity;
        int toAdd = Math.Min(spaceLeft, amount);
        Quantity += toAdd;
        return amount - toAdd; // returns leftover
    }

    public int Remove(int amount)
    {
        int toRemove = Math.Min(Quantity, amount);
        Quantity -= toRemove;
        return toRemove;
    }
}
