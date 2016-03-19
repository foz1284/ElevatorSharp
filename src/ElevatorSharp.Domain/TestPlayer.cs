using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorSharp.Domain
{
    public class TestPlayer : IPlayer
    {
        public void Update(IList<Elevator> elevators, IList<Floor> floors)
        {
            var elevator = elevators[0];
            if (elevator.CurrentFloor < floors.Count)
            {
                elevator.GoToFloor(elevator.CurrentFloor + 1);
            }
            else
            {
                elevator.GoToFloor(0);
            }
        }
    }
}