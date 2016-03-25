using System;
using System.Collections.Generic;

namespace ElevatorSharp.Domain.Players
{
    public class DevPlayer : IPlayer
    {
        public void Init(IList<Elevator> elevators, IList<Floor> floors)
        {
            foreach (var elevator in elevators)
            {
                elevator.Idle += Elevator_Idle; ;
                elevator.FloorButtonPressed += Elevator_FloorButtonPressed;
            }

            foreach (var floor in floors)
            {
                floor.UpButtonPressed += Floor_UpButtonPressed;
            }
        }

        private void Floor_UpButtonPressed(object sender, EventArgs e)
        {
            var floor = (Floor) sender;
            // TODO: var elevators = e.Elevators; // We need the elevators here because the player needs to decide which elevator to send to this floor.
        }

        private void Elevator_Idle(object sender, EventArgs e)
        {
            var elevator = (Elevator)sender;

            //elevator.GoToFloor(0);
            //elevator.GoToFloor(1);
            //elevator.GoToFloor(2);
        }

        private void Elevator_FloorButtonPressed(object sender, int e)
        {
            var elevator = (Elevator)sender;

            // Check if we're not already going to that floor
            if (!elevator.DestinationQueue.Contains(e))
            {
                // Go to the floor this passenger wants to go
                elevator.GoToFloor(e);
            }
        }

        

        public void Update(IList<Elevator> elevators, IList<Floor> floors)
        {
            // We normally don't need to do anything here
        }
    }
}