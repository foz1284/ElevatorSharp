using System;
using System.Collections.Generic;

namespace ElevatorSharp.Domain
{
    public sealed class Elevator : IElevator
    {
        public event EventHandler Idle;

        #region Properties
        public Queue<int> DestinationQueue { get; set; }
        #endregion

        #region Constructors
        public Elevator()
        {
            DestinationQueue = new Queue<int>();
        } 
        #endregion

        #region Methods
        public void GoToFloor(int floor)
        {
            //DestinationQueue.Push(floor);
        }
        #endregion

        private void OnIdle()
        {
            Idle?.Invoke(this, EventArgs.Empty);
        }
    }
}