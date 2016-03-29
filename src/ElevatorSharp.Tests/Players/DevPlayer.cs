using System;
using System.Collections.Generic;
using System.Linq;
using ElevatorSharp.Domain;
using ElevatorSharp.Game;

namespace ElevatorSharp.Tests.Players
{
    public class DevPlayer : IPlayer
    {
        #region Implementation of IPlayer
        public void Init(IElevator[] elevators, IFloor[] floors)
        {
            foreach (var elevator in elevators)
            {
                elevator.Idle += Elevator_Idle; ;
                //elevator.FloorButtonPressed += Elevator_FloorButtonPressed;
                //elevator.StoppedAtFloor += Elevator_StoppedAtFloor;
            }

            foreach (var floor in floors)
            {
                //floor.UpButtonPressed += Floor_UpButtonPressed;
                //floor.DownButtonPressed += Floor_DownButtonPressed;
            }
        }

        public void Update(IElevator[] elevators, IFloor[] floors)
        {
            // We normally don't need to do anything here
        }
        #endregion
        
        #region Event Handlers
        private void Floor_DownButtonPressed(object sender, IList<IElevator> elevators)
        {
            var floor = (Floor)sender;

            // Just pick first elevator to start with and go to the floor the button was pressed.
            // Could check which floor each elevator is currently on and which direction they are travelling?

            // grab the first elevator that is going up
            var elevator = elevators.FirstOrDefault(e => e.DestinationDirection == ElevatorDirection.Stopped);
            if (elevator != null && !elevator.DestinationQueue.Contains(floor.FloorNum))
            {
                elevator.GoToFloor(floor.FloorNum);
            }
        }

        private void Floor_UpButtonPressed(object sender, IList<IElevator> elevators)
        {
            var floor = (Floor)sender;

            // Just pick first elevator to start with and go to the floor the button was pressed.
            // Could check which floor each elevator is currently on and which direction they are travelling?

            // grab the first elevator that is going up
            var elevator = elevators.FirstOrDefault(e => e.DestinationDirection == ElevatorDirection.Stopped);
            if (elevator != null && !elevator.DestinationQueue.Contains(floor.FloorNum))
            {
                elevator.GoToFloor(floor.FloorNum); 
            }
        }

        private void Elevator_Idle(object sender, EventArgs e)
        {
            var elevator = (Elevator)sender;

            elevator.GoToFloor(0);
            elevator.GoToFloor(1);
            elevator.GoToFloor(2);
            elevator.GoToFloor(3);
            elevator.GoToFloor(4);
            elevator.GoToFloor(5);
            elevator.GoToFloor(6);
            elevator.GoToFloor(7);
        }

        private void Elevator_FloorButtonPressed(object sender, int e)
        {
            var elevator = (IElevator)sender;

            // If the elevator is full, then drop off some dudes first by jumping the queue
            if (elevator.LoadFactor > 0.6M)
            {
                elevator.GoToFloor(e, true); // jump queue
            }

            // Check if we're not already going to that floor
            if (!elevator.DestinationQueue.Contains(e))
            {
                // Go to the floor this passenger wants to go
                elevator.GoToFloor(e);
            }
        }

        private void Elevator_StoppedAtFloor(object sender, int e)
        {
            var elevator = (Elevator)sender;

            // remember that elevator.GoingUp(and Down)Indicator influences if passengers get on.

            // TODO: Do something here?
            // A passenger will only get on if the
            // indicator was pointing in the direction in which
            // they want to travel.
        }
        #endregion
    }
}