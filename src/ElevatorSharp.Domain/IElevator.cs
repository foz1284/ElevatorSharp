using System.Collections.Generic;

namespace ElevatorSharp.Domain
{
    public interface IElevator
    {
        void GoToFloor(int floor);
        Queue<int> DestinationQueue { get; set; }
    }
}