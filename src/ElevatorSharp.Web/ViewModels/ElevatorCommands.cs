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
            GoToFloors = new Queue<GoToFloorCommand>();
        }

        public Queue<GoToFloorCommand> GoToFloors { get; set; }
        public Queue<SetElevatorUpIndicator> SetElevatorUpIndicators { get; set; }
        public Queue<SetElevatorDownIndicator> SetElevatorDownIndicators { get; set; }
        public StopElevator[] StopElevators { get; set; }
    }
}