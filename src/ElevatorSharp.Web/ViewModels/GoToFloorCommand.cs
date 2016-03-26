namespace ElevatorSharp.Web.ViewModels
{
    public class GoToFloorCommand
    {
        public GoToFloorCommand(int elevatorIndex, int floorNumber, bool jumpQueue)
        {
            ElevatorIndex = elevatorIndex;
            FloorNumber = floorNumber;
            JumpQueue = jumpQueue;
        }

        public int ElevatorIndex { get; }
        public bool JumpQueue { get; }
        public int FloorNumber { get; }
    }
}