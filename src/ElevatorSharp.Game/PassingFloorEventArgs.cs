using System;

namespace ElevatorSharp.Game
{
    public class PassingFloorEventArgs : EventArgs
    {
        public int PassingFloorNumber { get; set; }
        public ElevatorDirection Direction { get; set; }
    }
}