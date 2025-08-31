using System.Numerics;

public class DroppedItemManager
{
    private List<DroppedItem> _droppedItems;

    public DroppedItemManager()
    {
        _droppedItems = new List<DroppedItem>();
    }

    public void AddDroppedItem(ItemStack itemStack, int x, int y)
    {
        _droppedItems.Add(new DroppedItem(itemStack, new Vector2(x, y)));
        
    }

    public void AddThrownDroppedItem(ItemStack itemStack, Vector2 positionToThrowFrom, Direction direction)
    {
        _droppedItems.Add(new DroppedItem(itemStack, positionToThrowFrom, 30, direction));
    }

    public void RemoveDroppedItem(DroppedItem item)
    {
        _droppedItems.Remove(item);
    }

    public void Update(float deltaTime)
    {
        for (int i = _droppedItems.Count - 1; i >= 0; i--)
        {
            DroppedItem droppedItem = _droppedItems[i];
            if (droppedItem.IsAlive)
            {
                droppedItem.Update(deltaTime);
            }
            if (!droppedItem.IsAlive)
            {
                _droppedItems.RemoveAt(i);
            }
        }
    }

    public void Draw()
    {
        foreach (var droppedItem in _droppedItems)
        {
            if (droppedItem.IsAlive)
            {
                droppedItem.Draw();
            }
        }
    }
}