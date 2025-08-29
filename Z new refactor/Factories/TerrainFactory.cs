public static class TerrainFactory
{
    public static Dictionary<string, ItemPlaceable> Terrains { get; private set; } = new()
    {
        ["terrain_grass"] = new ItemPlaceableTerrain("terrain_grass", "Grass", "Grassy.", 99),
    };
}