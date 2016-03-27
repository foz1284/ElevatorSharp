using System;
using System.Collections.Generic;

namespace ElevatorSharp.Domain.Players
{
    public class TestPlayer : IPlayer
    {
        public void Init(IList<IElevator> elevators, IList<IFloor> floors)
        {
            var elevator = elevators[0];
            elevator.Idle += ElevatorOnIdle;
        }
        
        private void ElevatorOnIdle(object sender, EventArgs eventArgs)
        {
            var elevator = (IElevator) sender;

            elevator.GoToFloor(0);
            elevator.GoToFloor(1);
            elevator.GoToFloor(2);
        }

        public void Update(IList<IElevator> elevators, IList<IFloor> floors)
        {
            // We normally don't need to do anything here
        }
    }
}