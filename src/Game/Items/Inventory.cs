public class Inventory
{
    public int Size { get; private set; }
    public ItemStack[] Slots { get; private set; }

    public Inventory(int size)
    {
        Size = size;
        Slots = new ItemStack[size]; // all slots initialized to null by default
    }

    // Try to add item to inventory
    public bool AddItem(Item item, int quantity = 1)
    {
        if (!item.CanBeInInventory) return false;

        // First try to add to existing stack
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i] != null && Slots[i].Item.GetType() == item.GetType() && !Slots[i].IsFull)
            {
                quantity = Slots[i].Add(quantity);
                if (quantity <= 0) return true;
            }
        }

        // Then try to add to empty slot
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i] == null)
            {
                int toAdd = Math.Min(quantity, 99); // default max stack
                Slots[i] = new ItemStack(item, toAdd);
                quantity -= toAdd;
                if (quantity <= 0) return true;
            }
        }

        // If leftover remains, couldn't fully add
        return quantity <= 0;
    }

    public bool RemoveItem(Item item, int quantity = 1)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i] != null && Slots[i].Item.GetType() == item.GetType())
            {
                int removed = Slots[i].Remove(quantity);
                quantity -= removed;

                if (Slots[i].Quantity == 0)
                    Slots[i] = null;

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
        return Slots[index];
    }
}
