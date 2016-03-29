using System;
using System.Collections.Generic;
using ElevatorSharp.Domain;

namespace ElevatorSharp.Game
{
    public interface IElevator
    {
        event EventHandler Idle;
        event EventHandler<int> FloorButtonPressed;
        event EventHandler PassingFloor;
        event EventHandler<int> StoppedAtFloor;

        /// <summary>
        /// Queue the elevator to go to specified floor number. 
        /// If you specify true as second argument, the elevator will go to that floor directly, and then go to any other queued floors.
        /// </summary>
        /// <param name="floor">Floor level.</param>
        void GoToFloor(int floor);

        /// <summary>
        /// Queue the elevator to go to specified floor number. 
        /// If you specify true as second argument, the elevator will go to that floor directly, and then go to any other queued floors.
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="jumpQueue"></param>
        void GoToFloor(int floor, bool jumpQueue);

        /// <summary>
        /// Clear the destination queue and stop the elevator if it is moving. Note that you normally don't need to stop elevators - it is intended for advanced solutions with in-transit rescheduling logic. Also, note that the elevator will probably not stop at a floor, so passengers will not get out.
        /// </summary>
        void Stop();

        /// <summary>
        /// Gets the currently pressed floor numbers as an array.
        /// Maybe go to some chosen floor first?
        /// </summary>
        /// <returns></returns>
        int[] PressedFloors { get; set; }

        /// <summary>
        /// Checks the destination queue for any new destinations to go to. Note that you only need to call this if you modify the destination queue explicitly.
        /// This method dequeues floor levels and goes to next floor in queue.
        /// </summary>
        void CheckDestinationQueue();

        /// <summary>
        /// Gets the floor number that the elevator currently is on.
        /// </summary>
        int CurrentFloor { get; set; }

        /// <summary>
        /// Gets or sets the going up indicator, which will affect passenger behaviour when stopping at floors.
        /// </summary>
        bool GoingUpIndicator { get; set; }

        /// <summary>
        /// Gets or sets the going down indicator, which will affect passenger behaviour when stopping at floors.
        /// </summary>
        bool GoingDownIndicator { get; set; }

        /// <summary>
        /// Gets the maximum number of passengers that can occupy the elevator at the same time.
        /// If it's above five, then maybe use this for something special, because it's big?
        /// </summary>
        int MaxPassengerCount { get; }

        /// <summary>
        /// Gets the load factor of the elevator. 0 means empty, 1 means full. Varies with passenger weights, which vary - not an exact measure.
        /// </summary>
        decimal LoadFactor { get; set; }

        /// <summary>
        /// Gets the direction the elevator is currently going to move toward. Can be "Up", "Down" or "Stopped".
        /// </summary>
        ElevatorDirection DestinationDirection { get; }

        /// <summary>
        /// The current destination queue, meaning the floor numbers the elevator is scheduled to go to. Can be modified and emptied if desired. Note that you need to call checkDestinationQueue() for the change to take effect immediately.
        /// </summary>
        Queue<int> DestinationQueue { get; }
    }
}