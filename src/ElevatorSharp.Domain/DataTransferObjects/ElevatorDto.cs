namespace ElevatorSharp.Domain.DataTransferObjects
{
    public class ElevatorDto
    {
        public int ElevatorIndex { get; set; }
        public int[] DestinationQueue { get; set; }
        public int CurrentFloor { get; set; }
        public bool GoingUpIndicator { get; set; }
        public bool GoingDownIndicator { get; set; }
        public int MaxPassengerCount { get; set; }
        public decimal LoadFactor { get; set; }
        public ElevatorDirection DestinationDirection { get; set; }
        public int[] PressedFloors { get; set; }
        public int FloorNumberPressed { get; set; }
        public ElevatorDirection Direction { get; set; }
        public int StoppedAtFloorNumber { get; set; }
    }
}