using System.Collections.Generic;

namespace ElevatorSharp.Domain
{
    public class Elevator : IElevator
    {
        public Elevator()
        {
            DestinationQueue = new Stack<int>();
        }

        #region Properties
        public Stack<int> DestinationQueue { get; set; } 
        #endregion

        #region Methods
        public void GoToFloor(int floor)
        {
            DestinationQueue.Push(floor);
        } 
        #endregion
    }
}