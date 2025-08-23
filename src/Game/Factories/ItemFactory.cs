public static class ItemFactory
{
    public static Dictionary<string, Item> Items { get; private set; } = new()
    {
        ["item_regular_sword"] = new ItemRegular("item_regular_sword", "Sword", "A sword.", 1),
        ["item_regular_shield"] = new ItemRegular("item_regular_shield", "Shield", "A sturdy shield.", 1)
    };
}