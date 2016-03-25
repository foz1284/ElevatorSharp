﻿using System;
using System.Collections.Generic;

namespace ElevatorSharp.Domain
{
    public sealed class Elevator : IElevator, IClientSideTracking
    {
        #region Events
        public event EventHandler Idle;
        public event EventHandler<int> FloorButtonPressed;
        public event EventHandler PassingFloor;
        public event EventHandler StoppedAtFloor; 
        #endregion

        #region Properties
        public ElevatorDirection DestinationDirection { get; set; }
        public Queue<int> DestinationQueue { get; set; }
        public int Index { get; }
        public Queue<int> NewDestinations { get; set; }
        public Queue<int> JumpQueueDestinations { get; set; }

        public int CurrentFloor { get; set; }
        public bool GoingUpIndicator { get; set; }
        public bool GoingDownIndicator { get; set; }
        public int MaxPassengerCount { get; }
        public int[] PressedFloors { get; set; }
        #endregion

        #region Constructors
        public Elevator(int index, int maxPassengerCount)
        {
            Index = index;
            MaxPassengerCount = maxPassengerCount;
            DestinationQueue = new Queue<int>();
            NewDestinations = new Queue<int>();
            JumpQueueDestinations = new Queue<int>();
        }
        #endregion

        #region Event Invocators
        public void OnIdle()
        {
            Idle?.Invoke(this, EventArgs.Empty);
        }

        public void OnFloorButtonPressed(int floorNumber)
        {
            FloorButtonPressed?.Invoke(this, floorNumber);
        }

        public void OnPassingFloor()
        {
            PassingFloor?.Invoke(this, EventArgs.Empty);
        }

        public void OnStoppedAtFloor()
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
            GoToFloor(floor, false);
        }

        /// <summary>
        /// Queue the elevator to go to specified floor number. If you specify true as second argument, the elevator will go to that floor directly, and then go to any other queued floors.
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="jumpQueue"></param>
        public void GoToFloor(int floor, bool jumpQueue)
        {
            if (!jumpQueue)
            {
                DestinationQueue.Enqueue(floor);
                NewDestinations.Enqueue(floor);
            }
            else
            {
                var items = DestinationQueue.ToArray();
                DestinationQueue.Clear();
                DestinationQueue.Enqueue(floor);
                foreach (var item in items)
                    DestinationQueue.Enqueue(item);

                JumpQueueDestinations.Enqueue(floor);
            }
        }

        /// <summary>
        /// Checks the destination queue for any new destinations to go to. 
        /// Note that you only need to call this if you modify the destination queue explicitly.
        /// </summary>
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

        /// <summary>
        /// Clear the destination queue and stop the elevator if it is moving. 
        /// Note that you normally don't need to stop elevators - it is intended for advanced solutions with in-transit rescheduling logic. 
        /// Also, note that the elevator will probably not stop at a floor, so passengers will not get out.
        /// </summary>
        public void Stop()
        {
            // TODO: We need a StopCommand on the controller and we need to communicate this back from here
            throw new NotImplementedException();
        }
        #endregion
        
    }
}