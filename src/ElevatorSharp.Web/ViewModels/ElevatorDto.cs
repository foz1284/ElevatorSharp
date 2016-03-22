using System.Collections.Generic;
using ElevatorSharp.Domain;

namespace ElevatorSharp.Web.ViewModels
{
    public class ElevatorDto
    {
        public Queue<int> DestinationQueue { get; set; }
        public int CurrentFloor { get; set; }
        public bool GoingUpIndicator { get; set; }
        public bool GoingDownIndicator { get; set; }
        public int MaxPassengerCount { get; set; }
        public decimal LoadFactor { get; set; }
        public ElevatorDirection DestinationDirection { get; set; }
        public int[] PressedFloors { get; set; }
    }
}