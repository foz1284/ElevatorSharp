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

        // TODO: Could we send all elevators in EventArgs? Or how many floors there are?
        private void ElevatorOnIdle(object sender, EventArgs eventArgs)
        {
            var elevator = (Elevator) sender;

            elevator.GoToFloor(0);
            elevator.GoToFloor(1);
            elevator.GoToFloor(2);
            elevator.GoToFloor(3);
            elevator.GoToFloor(4);
        }

        public void Update(IList<Elevator> elevators, IList<Floor> floors)
        {
            // We normally don't need to do anything here
        }
    }
}