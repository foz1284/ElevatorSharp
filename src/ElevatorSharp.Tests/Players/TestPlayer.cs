using System;
using ElevatorSharp.Game;

namespace ElevatorSharp.Tests.Players
{
    public class TestPlayer : IPlayer
    {
        public void Init(IElevator[] elevators, IFloor[] floors)
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

        public void Update(IElevator[] elevators, IFloor[] floors)
        {
            // We normally don't need to do anything here
        }
    }
}