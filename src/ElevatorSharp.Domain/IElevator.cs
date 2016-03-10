using System.Collections.Generic;

namespace ElevatorSharp.Domain
{
    public interface IElevator
    {
        void GoToFloor(int floor);
        Stack<int> DestinationQueue { get; set; }
    }
}