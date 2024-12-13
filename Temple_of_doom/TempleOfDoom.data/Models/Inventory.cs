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
}