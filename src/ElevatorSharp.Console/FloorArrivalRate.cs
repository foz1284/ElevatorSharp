using System;
using System.Collections.Generic;

namespace ElevatorSharp.Console
{
    internal class FloorArrivalRate
    {
        internal int Floor { get; set; }
        internal TimeSpan StartTime { get; set; }
        internal TimeSpan EndTime { get; set; }
        public int msPerArrival { get; internal set; }
        public TimeSpan LastArrival { get; internal set; }

        internal List<int> DestinationWeights = new List<int>();
    }
}
