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

        public Queue<GoToFloorCommand> GoToFloor { get; set; }
    }
}