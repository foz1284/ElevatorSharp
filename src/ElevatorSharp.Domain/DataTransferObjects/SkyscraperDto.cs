using System.Collections.Generic;

namespace ElevatorSharp.Domain.DataTransferObjects
{
    public class SkyscraperDto
    {
        public List<ElevatorDto> Elevators { get; set; }
        public List<FloorDto> Floors { get; set; }

        public int EventRaisedElevatorIndex { get; set; }
        public int EventRaisedFloorNumber { get; set; }
    }
}