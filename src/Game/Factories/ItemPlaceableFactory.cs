public static class ItemPlaceableFactory
{
    public static Dictionary<string, ItemPlaceable> PlaceableItems { get; private set; } = new()
    {
        ["terrain_grass"] = new ItemPlaceableTerrain("terrain_grass", "Grass", "Grassy.", 99),
        ["floor_wood"] = new ItemPlaceableFloor("floor_wood", "Wood", "Woody.", 99, "item_placeable_wooden_floor.png"),
        ["wall_stone"] = new ItemPlaceableObject("wall_stone", "Stone Wall", "Stony.", 99)
    };
}
