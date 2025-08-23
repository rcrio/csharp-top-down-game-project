public static class ItemPlaceableFactory
{
    public static Dictionary<string, ItemPlaceable> PlaceableItems { get; private set; } = new()
    {
        ["terrain_grass"] = new ItemPlaceableTerrain("terrain_grass", "Grass", "Grassy.", 1),
        ["floor_wood"] = new ItemPlaceableFloor("floor_wood", "Wood", "Woody.", 1),
        ["wall_stone"] = new ItemPlaceableObject("wall_stone", "Stone Wall", "Stony.", 1)
    };
}
