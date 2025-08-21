using Raylib_cs;

public abstract class ItemPlaceable : Item
{
    public bool IsWalkable { get; set; }
    protected ItemPlaceable(string name, string description, Texture2D sprite) : base(name, description, sprite)
    {

    }
    public abstract void Draw(int x, int y);
}