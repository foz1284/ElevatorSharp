using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorSharp.Domain
{
    public class TestPlayer : IPlayer
    {
        public void Init(IEnumerable<Elevator> elevators, IEnumerable<Floor> floors)
        {
            var elevator = elevators.First();
            elevator.Idle += OnIdle;
        }

        private void OnIdle(object sender, EventArgs e)
        {
            var elevator = (Elevator) sender;
            elevator.GoToFloor(0);
            elevator.GoToFloor(1);
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}