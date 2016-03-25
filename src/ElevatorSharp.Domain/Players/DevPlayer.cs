using System;
using System.Collections.Generic;

namespace ElevatorSharp.Domain.Players
{
    public class DevPlayer : IPlayer
    {
        public void Init(IList<Elevator> elevators, IList<Floor> floors)
        {
            var elevator = elevators[0];
            elevator.Idle += Elevator_Idle; ;
            elevator.FloorButtonPressed += Elevator_FloorButtonPressed;
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

            // Go to the floor this passenger wants to go
            elevator.GoToFloor(e);
        }

        

        public void Update(IList<Elevator> elevators, IList<Floor> floors)
        {
            // We normally don't need to do anything here
        }
    }
}