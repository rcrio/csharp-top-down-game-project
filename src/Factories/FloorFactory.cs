public static class FloorFactory
{
    public static Dictionary<string, ItemPlaceable> Floors { get; private set; } = new()
    {
        ["floor_wood"] = new ItemPlaceableFloor("floor_wood", "Wood", "Woody.", 99, "item_placeable_wooden_floor.png"),
    };
}