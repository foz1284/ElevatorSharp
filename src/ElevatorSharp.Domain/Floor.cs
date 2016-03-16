namespace ElevatorSharp.Domain
{
    public class Floor : IFloor
    {
        public Floor(int level)
        {
            Level = level;
        }

        public int Level { get; }

        public void PressDownButton()
        {
            throw new System.NotImplementedException();
        }

        public void PressUpButton()
        {
            throw new System.NotImplementedException();
        }
    }
}