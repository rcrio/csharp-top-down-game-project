
public abstract class ItemPlaceable : Item
{
    public bool IsWalkable { get; set; }
    public ItemPlaceable(string id, string name, string description, int maxStack, string spritePath = null) : base(id, name, description, maxStack, spritePath) { } 
    public abstract void Draw(int x, int y);
    

}