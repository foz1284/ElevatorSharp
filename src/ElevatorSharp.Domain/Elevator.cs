﻿using System;
using System.Collections.Generic;

namespace ElevatorSharp.Domain
{
    public sealed class Elevator : IElevator
    {
        #region Events
        public event EventHandler Idle;
        public event EventHandler FloorButtonPressed;
        public event EventHandler PassingFloor;
        public event EventHandler StoppedAtFloor; 
        #endregion

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

        #region Private Methods
        private void OnIdle()
        {
            Idle?.Invoke(this, EventArgs.Empty);
        }

        private void OnFloorButtonPressed()
        {
            FloorButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        private void OnPassingFloor()
        {
            PassingFloor?.Invoke(this, EventArgs.Empty);
        }

        private void OnStoppedAtFloor()
        {
            StoppedAtFloor?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Queue the elevator to go to specified floor number. If you specify true as second argument, the elevator will go to that floor directly, and then go to any other queued floors.
        /// </summary>
        /// <param name="floor"></param>
        public void GoToFloor(int floor)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Queue the elevator to go to specified floor number. If you specify true as second argument, the elevator will go to that floor directly, and then go to any other queued floors.
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="jumpQueue"></param>
        public void GoToFloor(int floor, bool jumpQueue)
        {
            throw new NotImplementedException();
        }

        public void CheckDestinationQueue()
        {
            if (DestinationQueue.Count > 0)
            {
                var nextFloor = DestinationQueue.Dequeue();
                GoToFloor(nextFloor);
            }
            else
            {
                OnIdle();
            }
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