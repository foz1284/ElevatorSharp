using System;
using System.Collections.Generic;

namespace ElevatorSharp.Domain.Players
{
    public class DevPlayer : IPlayer
    {
        public void Init(IList<IElevator> elevators, IList<IFloor> floors)
        {
            foreach (var elevator in elevators)
            {
                elevator.Idle += Elevator_Idle; ;
                elevator.FloorButtonPressed += Elevator_FloorButtonPressed;
            }

            foreach (var floor in floors)
            {
                floor.UpButtonPressed += Floor_UpButtonPressed;
                floor.DownButtonPressed += Floor_DownButtonPressed;
            }
        }

        private void Floor_DownButtonPressed(object sender, IList<IElevator> elevators)
        {
            var floor = (Floor)sender;

            // Just pick first elevator to start with and go to the floor the button was pressed.
            // Could check which floor each elevator is currently on and which direction they are travelling?
            //var elevator = elevators[0];
            //elevator.GoToFloor(floor.FloorNum);
        }

        private void Floor_UpButtonPressed(object sender, IList<IElevator> elevators)
        {
            var floor = (Floor) sender;

            // Just pick first elevator to start with and go to the floor the button was pressed.
            // Could check which floor each elevator is currently on and which direction they are travelling?
            //var elevator = elevators[0];
            //elevator.GoToFloor(floor.FloorNum);
        }

        private void Elevator_Idle(object sender, EventArgs e)
        {
            var elevator = (Elevator)sender;

            elevator.GoToFloor(0);
            elevator.GoToFloor(1);
            elevator.GoToFloor(2);
        }

        private void Elevator_FloorButtonPressed(object sender, int e)
        {
            var elevator = (IElevator)sender;

            // Check if we're not already going to that floor
            //if (!elevator.DestinationQueue.Contains(e))
            //{
            //    // Go to the floor this passenger wants to go
            //    elevator.GoToFloor(e);
            //}
        }

        

        public void Update(IList<IElevator> elevators, IList<IFloor> floors)
        {
            // We normally don't need to do anything here
        }
    }
}