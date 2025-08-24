public static class ItemFactory
{
    public static Dictionary<string, Item> Items { get; private set; } = new()
    {
        ["item_regular_wooden_pickaxe"] = new ItemRegular("item_regular_wooden_pickaxe", "Wooden Pickaxe", "A wooden pickaxe.", 1, "item_regular_wooden_pickaxe.png"),
        ["item_regular_wooden_sword"] = new ItemRegular("item_regular_wooden_sword", "Wooden Sword", "A wooden sword.", 1, "item_regular_wooden_sword.png"),
    };
}