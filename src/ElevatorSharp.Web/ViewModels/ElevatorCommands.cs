using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace ElevatorSharp.Web.ViewModels
{
    public class ElevatorCommands
    {
        public ElevatorCommands()
        {
            GoToFloor = new Queue<GoToFloorCommand>();
        }

        public int ElevatorIndex { get; set; }
        public Queue<GoToFloorCommand> GoToFloor { get; set; }
    }

    public class GoToFloorCommand
    {
        public GoToFloorCommand(int floorNumber, bool jumpQueue)
        {
            FloorNumber = floorNumber;
            JumpQueue = jumpQueue;
        }

        public int FloorNumber { get; set; }
        public bool JumpQueue { get; set; }
    }
}