namespace ElevatorSharp.Domain
{
    public class Floor : IFloor
    {
        public Floor(int level)
        {
            Level = level;
        }

        public int Level { get; }
    }
}