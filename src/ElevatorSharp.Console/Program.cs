using System;
using ElevatorSharp.Domain;
using ElevatorSharp.Domain.DataTransferObjects;
using ElevatorSharp.Tests.Players;
using System.Linq;
using System.Collections.Generic;

namespace ElevatorSharp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (Level level in GetLevels())
            {
                SkyscraperDto skyscraperDto = new SkyscraperDto
                {
                    Floors = new List<FloorDto> (),
                    Elevators = new List<ElevatorDto> ()
                };

                for (int i = 0; i < level.FloorCount; i++)
                {
                    skyscraperDto.Floors.Add(new FloorDto { FloorNumber = i });
                }

                for (int i = 0; i < level.ElevatorCount; i++)
                {
                    skyscraperDto.Elevators.Add(new ElevatorDto { CurrentFloor = 0, MaxPassengerCount = level.MaxPassengerCount });
                }

                var skyscraper = new Skyscraper(skyscraperDto);
                var player = new Dev2Player();
                skyscraper.LoadPlayer(player);

                InitialiseGame(skyscraper, level);
            }
        }

        private static IEnumerable<Level> GetLevels()
        {
            return new List<Level> {
                //new Level { Name = "Level 0", MaxPassengerCount = 1, ElevatorCount = 1, FloorCount = 2, NumberOfPassengers = 5, TimeLimit = 2},
                new Level { Name = "Level 0", MaxPassengerCount = 1, ElevatorCount = 1, FloorCount = 2, NumberOfPassengers = 10, TimeLimit = 60},
                //new Level { Name = "Level 1", MaxPassengerCount = 8, ElevatorCount = 1, FloorCount = 3, NumberOfPassengers = 1000, TimeLimit = 100},
                //new Level { Name = "Level 2", MaxPassengerCount =30, ElevatorCount = 2, FloorCount = 3, NumberOfPassengers = 10000, TimeLimit = 1000}
            };
        }

        private static void InitialiseGame(Skyscraper skyscraper, Level level)
        {
            var gametime = new TimeSpan();
            for (int i = 0; i < level.NumberOfPassengers; i++)
            {
                skyscraper.Floors[0].PassengerArrives(skyscraper, new Passenger(1));
                //TODO: Generate Passengers in more realistic way
            }

            while (gametime.Seconds < level.TimeLimit)
            {
                gametime = gametime.Add(new TimeSpan(0, 0, 0, 0, 1));

                Update(gametime, skyscraper);

                Render(gametime, skyscraper);

                if (level.WinCriteriaMet(skyscraper))
                {
                    System.Console.WriteLine("You Win:" + level.Name + ":" + gametime);
                    foreach (var elevator in skyscraper.Elevators)
                    {
                        System.Console.WriteLine(elevator.UnloadedCount);
                    }
                    
                    System.Console.WriteLine("Press Any Key To Close:");
                    System.Console.ReadLine();
                    break;
                }
            }

            if (!level.WinCriteriaMet(skyscraper))
            {
                System.Console.WriteLine("You Lose:" + level.Name + ":" + gametime);
                foreach (var elevator in skyscraper.Elevators)
                {
                    System.Console.WriteLine(elevator.UnloadedCount);
                }

                System.Console.WriteLine("Total Transported:" + skyscraper.Elevators.Sum(e => e.UnloadedCount));
                System.Console.WriteLine("Press Any Key To Close:");
                System.Console.ReadLine();
            }
        }

        private static void Update(TimeSpan gameTime, Skyscraper skyscraper)
        {
            skyscraper.Update(gameTime);
        }

        private static void Render(TimeSpan gametime, Skyscraper skyscraper)
        {
            //System.Console.WriteLine(skyscraper.Floors[0].PassengersWaiting.Count);
            System.Console.WriteLine(skyscraper.Elevators[0].CurrentFloor  + " - "+ skyscraper.Elevators[0].HeightAboveCurrentFloor);
        }
    }
}