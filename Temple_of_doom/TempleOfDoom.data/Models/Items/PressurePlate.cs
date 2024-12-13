namespace TempleOfDoom.data.Models.Items;

public class PressurePlate : Item
{
    public PressurePlate()
    {
        IsActive = false;
    }

    public bool IsActive { get; set; }
    public event Action Activated;

    public void StepOn()
    {
        IsActive = true;
        Activated?.Invoke();
    }
}