using System;
using ElevatorSharp.Game;
using ElevatorSharp.Game.Players;

namespace ElevatorSharp.Tests.Players
{
    public class TestPlayer : IPlayer
    {
        public void Init(IElevator[] elevators, IFloor[] floors)
        {
            // Let's use the first elevator
            var elevator = elevators[0];

            // Whenever the elevator is idle (has no more queued destinations) ...
            elevator.Idle += ElevatorOnIdle;
        }

        private void ElevatorOnIdle(object sender, EventArgs eventArgs)
        {
            var elevator = (IElevator)sender;

            // let's go to all the floors (or did we forget one?)
            elevator.GoToFloor(0);
            elevator.GoToFloor(1);
        }

        public void Update(IElevator[] elevators, IFloor[] floors)
        {
            // We normally don't need to do anything here
        }
    }
}