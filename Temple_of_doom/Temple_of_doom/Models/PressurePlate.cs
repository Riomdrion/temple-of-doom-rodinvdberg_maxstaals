namespace Temple_of_doom.Models
{
    public class PressurePlate
    {
        public bool IsActive { get; set; }
        public event Action Activated;

        public void StepOn()
        {
            IsActive = true;
            Activated?.Invoke();
        }
    }

}
