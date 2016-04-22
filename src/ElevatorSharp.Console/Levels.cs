using System;
using System.Collections.Generic;

namespace ElevatorSharp.Console
{
    internal static class Levels
    {

        internal static List<Level> GetLevels()
        {
            List<Level> levels = new List<Level>();

            for (int i = 1; i == 1; i++)
            {
                levels.Add(GetLevel(i));
            }

            return levels;
        }

        internal static Level GetLevel(int level)
        {
            switch (level)
            {
                case 1:
                    return new Level
                    {
                        Name = "Level 0",
                        MaxPassengerCount = 6,
                        ElevatorCount = 1,
                        FloorCount = 3,
                        NumberOfPassengers = 100,
                        TimeLimit = 86400,
                        PassengerArrivalProfile = new PassengerArrivalProfile
                        {
                            FloorArrivalRate = new List<FloorArrivalRate>
                                    {
                                        new FloorArrivalRate {
                                            Floor = 0,
                                            msPerArrival = 100000,
                                            StartTime = new TimeSpan(0),
                                            EndTime = new TimeSpan(999,0,0,0),
                                            DestinationWeights = new List<int> { 0,0,100 }
                                        }
                                    }
                        }
                    };
                default:
                    throw new NotImplementedException();
            }

            //new Level { Name = "Level 0", MaxPassengerCount = 1, ElevatorCount = 1, FloorCount = 2, NumberOfPassengers = 5, TimeLimit = 2},
            //new Level { Name = "Level 1", MaxPassengerCount = 8, ElevatorCount = 1, FloorCount = 3, NumberOfPassengers = 1000, TimeLimit = 100},
            //new Level { Name = "Level 2", MaxPassengerCount =30, ElevatorCount = 2, FloorCount = 3, NumberOfPassengers = 10000, TimeLimit = 1000}

        }
    }
}
