using Raylib_cs;

public abstract class ItemPlaceable : Item
{
    public bool IsWalkable { get; set; }
    public ItemPlaceable(string id, string name, string description, Texture2D sprite = default) : base(id, name, description, sprite)
    {

    }
    public abstract void Draw(int x, int y);
}