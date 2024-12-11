namespace Temple_of_doom.Models
{
    public class ToggleDoor : Door
    {
        public bool IsLocked { get; set; }
        public ToggleDoor()
        {
        }

        public override void Open()
        {
            throw new NotImplementedException();
        }
    }

}
