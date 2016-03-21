using System.Collections.Generic;

namespace ElevatorSharp.Web.ViewModels
{
    public class IdleViewModel
    {
        public Queue<int> DestinationQueue { get; set; }
        public Queue<int> GoToFloors { get; set; }
    }
}