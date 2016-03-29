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
            SetUpIndicators = new List<SetIndicatorCommand>();
            SetDownIndicators = new List<SetIndicatorCommand>();
            StopElevators = new List<StopElevator>();
        }

        public Queue<GoToFloorCommand> GoToFloors { get; set; }
        public List<SetIndicatorCommand> SetUpIndicators { get; set; }
        public List<SetIndicatorCommand> SetDownIndicators { get; set; }
        public List<StopElevator> StopElevators { get; set; }
    }
}