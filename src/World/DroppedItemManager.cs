public class DroppedItemManager
{
    private List<DroppedItem> _droppedItems;

    public DroppedItemManager()
    {
        _droppedItems = new List<DroppedItem>();
    }

    public void AddDroppedItem(Item item)
    {
        //_droppedItems.Add(new DroppedItem(item));
    }

    public void RemoveDroppedItem(DroppedItem item)
    {
        _droppedItems.Remove(item);
    }

    public void Update(float deltaTime)
    {
        foreach (var droppedItem in _droppedItems)
        {
            droppedItem.Update(deltaTime);
        }
    }

    public void Draw(int x, int y)
    {
        foreach (var droppedItem in _droppedItems)
        {
            droppedItem.Draw(x, y);
        }
    }
}