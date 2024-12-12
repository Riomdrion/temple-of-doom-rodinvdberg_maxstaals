namespace Temple_of_doom.Models
{
    public class PressurePlate : Item
    {
        public PressurePlate(string? toString)
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

}
