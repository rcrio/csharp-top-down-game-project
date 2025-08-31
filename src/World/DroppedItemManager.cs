

using System.Numerics;
using Raylib_cs;

public class DroppedItemManager
{
    private List<DroppedItem> _droppedItems;
    public List<DroppedItem> ItemsToCollect;

    public DroppedItemManager()
    {
        _droppedItems = new List<DroppedItem>();
        ItemsToCollect = new List<DroppedItem>();
    }

    public void AddDroppedItem(ItemStack itemStack, int x, int y)
    {
        _droppedItems.Add(new DroppedItem(itemStack, new Vector2(x, y)));
        
    }

    public void AddThrownDroppedItem(ItemStack itemStack, Vector2 positionToThrowFrom, Direction direction)
    {
        _droppedItems.Add(new DroppedItem(itemStack, positionToThrowFrom, 2, 30, direction));
    }

    public void RemoveDroppedItem(DroppedItem item)
    {
        _droppedItems.Remove(item);
    }

    public List<DroppedItem> GetDroppedItemsInRadius(Rectangle bounds) {
        List<DroppedItem> itemsToCollect = new List<DroppedItem>();
        foreach (DroppedItem droppedItem in _droppedItems)
        {
            if (droppedItem.CanCollect &&
                droppedItem.Position.X >= bounds.X &&
                droppedItem.Position.X <= bounds.X + bounds.Width &&
                droppedItem.Position.Y >= bounds.Y &&
                droppedItem.Position.Y <= bounds.Y + bounds.Height
            )
            {
                itemsToCollect.Add(droppedItem);
            }
        }

        return itemsToCollect;
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