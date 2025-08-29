public static class WallFactory
{
    public static Dictionary<string, ItemPlaceable> Walls { get; private set; } = new()
    {
        ["wall_stone"] = new ItemPlaceableObject("wall_stone", "Stone Wall", "Stony.", 99)
    };
}