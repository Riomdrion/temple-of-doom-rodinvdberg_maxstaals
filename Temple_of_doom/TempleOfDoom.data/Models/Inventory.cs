namespace TempleOfDoom.data.Models;

public class Inventory
{
    private readonly List<string> _items = new();

    public void AddItem(string item)
    {
        _items.Add(item);
    }

    public bool HasItem(string item)
    {
        return _items.Contains(item);
    }
    public int GetItemCount(string item)
    {
        return _items.Count(i => i == item);  // Return the count of specific items (e.g., Sankara Stone)
    }
}