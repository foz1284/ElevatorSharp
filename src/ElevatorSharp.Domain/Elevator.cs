using System;
using System.Collections.Generic;

namespace ElevatorSharp.Domain
{
    public sealed class Elevator : IElevator
    {
        #region Properties

        public ElevatorDirection DestinationDirection { get; }
        public Queue<int> DestinationQueue { get; set; }
        public int CurrentFloor { get; set; }
        public bool GoingUpIndicator { get; set; }
        public bool GoingDownIndicator { get; set; }
        public int MaxPassengerCount { get; }
        #endregion

        #region Constructors
        public Elevator(int maxPassengerCount)
        {
            MaxPassengerCount = maxPassengerCount;
        }
        #endregion

        #region Methods
        public void GoToFloor(int floor)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public int[] GetPressedFloors()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}