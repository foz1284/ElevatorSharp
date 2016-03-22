using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorSharp.Domain
{
    public class TestPlayer : IPlayer
    {
        public void Init(IList<Elevator> elevators, IList<Floor> floors)
        {
            var elevator = elevators[0];
            elevator.Idle += ElevatorOnIdle;
        }

        private void ElevatorOnIdle(object sender, EventArgs eventArgs)
        {
            var elevator = (Elevator) sender;
            if(elevator.PressedFloors == null) return;

            var floors = elevator.PressedFloors;
            foreach (var floor in floors)
            {
                elevator.GoToFloor(floor);
            }
        }

        public void Update(IList<Elevator> elevators, IList<Floor> floors)
        {
            // We normally don't need to do anything here
        }
    }
}