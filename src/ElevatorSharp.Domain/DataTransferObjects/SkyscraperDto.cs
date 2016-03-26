namespace ElevatorSharp.Domain.DataTransferObjects
{
    public class SkyscraperDto
    {
        public ElevatorDto[] Elevators { get; set; }
        public FloorDto[] Floors { get; set; }

        public int EventRaisedElevatorIndex { get; set; }
        public int EventRaisedFloorNumber { get; set; }
    }
}