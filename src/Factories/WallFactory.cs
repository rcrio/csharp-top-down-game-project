public static class WallFactory
{
    public static Dictionary<string, ItemPlaceable> Walls { get; private set; } = new()
    {
        ["wall_stone"] = new ItemPlaceableObject("wall_stone", "Stone Wall", "Stony.", 99),
        ["wall_wooden"] =  new ItemPlaceableObject("wall_wooden", "Wooden Wall", "A nice looking wooden wall.", 99, "item_placeable_wooden_wall.png")
    };
}