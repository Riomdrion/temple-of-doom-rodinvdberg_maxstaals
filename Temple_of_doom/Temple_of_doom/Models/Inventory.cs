namespace Temple_of_doom.Models;

public class Inventory
{
    private List<string> _items = new List<string>();

    public void AddItem(string item)
    {
        _items.Add(item);
    }

    public bool HasItem(string item)
    {
        return _items.Contains(item);
    }
}
