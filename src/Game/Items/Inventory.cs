public class Inventory
{
    public int Size { get; private set; }
    public ItemStack[] ItemStacks { get; private set; }

    public Inventory(int size)
    {
        Size = size;
        ItemStacks = new ItemStack[size]; // all slots initialized to null by default
    }

    // Try to add item to inventory
    public bool AddItem(Item item, int quantity = 1)
    {
        // Exit out the method if item cannot be added to inventory.
        if (!item.CanBeInInventory) return false;

        // First try to add to existing stack
        for (int i = 0; i < ItemStacks.Length; i++)
        {
            if (ItemStacks[i] != null && ItemStacks[i].Item.GetType() == item.GetType() && !ItemStacks[i].IsFull)
            {
                quantity = ItemStacks[i].Add(quantity);
                if (quantity <= 0) return true;
            }
        }

        // Then try to add to empty slot
        for (int i = 0; i < ItemStacks.Length; i++)
        {
            if (ItemStacks[i] == null)
            {
                int toAdd = Math.Min(quantity, 99); // default max stack
                ItemStacks[i] = new ItemStack(item, toAdd);
                quantity -= toAdd;
                if (quantity <= 0) return true;
            }
        }

        // If leftover remains, couldn't fully add
        return quantity <= 0;
    }

    public void AddItemByIndex(int index, Item item, int quantity = 1)
    {
        // Exit out the method if item cannot be added to inventory.
        if (!item.CanBeInInventory) return;

        // Exit if index is out of bounds.
        if (index > Size - 1) return;

        if (ItemStacks[index] != null && ItemStacks[index].Item.GetId() == item.GetId() && !ItemStacks[index].IsFull)
        {
            quantity = ItemStacks[index].Add(quantity);
            if (quantity <= 0) return;
        }
        else if (ItemStacks[index] == null) // create a new stack if empty
        {
            int toAdd = Math.Min(quantity, 99);
            ItemStacks[index] = new ItemStack(item, toAdd);
            Console.WriteLine("It happened!");
            quantity -= toAdd;
        }
    }

    public bool RemoveItem(Item item, int quantity = 1)
    {
        for (int i = 0; i < ItemStacks.Length; i++)
        {
            if (ItemStacks[i] != null && ItemStacks[i].Item.GetId() == item.GetId())
            {
                int removed = ItemStacks[i].Remove(quantity);
                quantity -= removed;

                if (ItemStacks[i].Quantity == 0)
                    ItemStacks[i] = null;

                if (quantity <= 0) return true;
            }
        }

        return quantity <= 0; // false if not enough items to remove
    }

    // Safely get the stack at a given index
    public ItemStack GetStack(int index)
    {
        if (index < 0 || index >= Size)
            return null; // out of bounds
        return ItemStacks[index];
    }
}
