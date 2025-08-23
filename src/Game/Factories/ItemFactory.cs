public static class ItemFactory
{
    public static Dictionary<string, Item> Items { get; private set; } = new()
    {
        ["item_regular_sword"] = new ItemRegular("item_regular_sword", "Sword", "A sword."),
        ["item_regular_shield"] = new ItemRegular("item_regular_shield", "Shield", "A sturdy shield.")
    };
}