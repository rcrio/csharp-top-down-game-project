public class Tile
{
    private ItemPlaceableTerrain _itemPlaceableTerrain;
    private ItemPlaceableFloor _itemPlaceableFloor;
    private ItemPlaceableObject _itemPlaceableObject;

    public Tile(ItemPlaceableTerrain itemPlaceableTerrain, ItemPlaceableFloor itemPlaceableFloor, ItemPlaceableObject itemPlaceableObject)
    {
        _itemPlaceableTerrain = itemPlaceableTerrain;
        _itemPlaceableFloor = itemPlaceableFloor;
        _itemPlaceableObject = itemPlaceableObject;
    }

    public void Draw(int x, int y)
    {
        _itemPlaceableTerrain.Draw(x, y);
        _itemPlaceableFloor?.Draw(x, y);
        _itemPlaceableObject?.Draw(x, y);
    }

    // Checks if current tile is walkable.
    public bool IsWalkable()
    {
        // Terrain can never be null.
        if (_itemPlaceableTerrain == null)
            throw new InvalidOperationException("Terrain cannot be null!");

        return _itemPlaceableTerrain.IsWalkable
        && (_itemPlaceableFloor?.IsWalkable ?? true)
        && (_itemPlaceableObject?.IsWalkable ?? true);
    }

    // Could refactor this to have info not be created every single time, and just updated. lets keep it like this for now
    // eventually, tilecell will need an update method of some sort so we can combine the implementation there
    public Dictionary<string, string> TileInformation()
    {
        Dictionary<string, string> info = new Dictionary<string, string>();

        info["Terrain Name"] = _itemPlaceableTerrain?.Name ?? "No terrain";
        info["Terrain Description"] = _itemPlaceableTerrain?.Description ?? "No terrain";

        info["Floor Name"] = _itemPlaceableFloor?.Name ?? "No floor";
        info["Floor Description"] = _itemPlaceableFloor?.Description ?? "No floor";

        info["Object Name"] = _itemPlaceableObject?.Name ?? "No wall, furniture or object";
        info["Object Description"] = _itemPlaceableObject?.Description ?? "No wall, furniture or object";

        return info;
    }
}
