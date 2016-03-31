using System.Collections.Generic;

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
            DestinationQueueCommands = new List<DestinationQueueCommand>();
        }

        public Queue<GoToFloorCommand> GoToFloors { get; set; }
        public List<SetIndicatorCommand> SetUpIndicators { get; set; }
        public List<SetIndicatorCommand> SetDownIndicators { get; set; }
        public List<StopElevator> StopElevators { get; set; }
        public List<DestinationQueueCommand> DestinationQueueCommands { get; set; } 

        public string Message { get; set; }
    }

    public class DestinationQueueCommand
    {
        public int ElevatorIndex { get; set; }
        public Queue<int> DestinationQueue { get; set; }
    }
}