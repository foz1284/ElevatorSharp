using System;
using ElevatorSharp.Domain;
using System.Linq;

namespace ElevatorSharp.Console
{
    internal class Level
    {
        public override string ToString()
        {
            return Name; 
        }
        public int ElevatorCount { get; internal set; }
        public int FloorCount { get; internal set; }
        public int MaxPassengerCount { get; internal set; }
        public string Name { get; internal set; }
        public int NumberOfRiders { get; internal set; }
        public int TimeLimit { get; internal set; }

        internal bool WinCriteriaMet(Skyscraper skyscraper)
        {
            return skyscraper.Elevators.Sum(e => e.UnloadedCount) >= NumberOfRiders;
        }
    }
}