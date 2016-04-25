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
            foreach (Level level in Levels.GetLevels())
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

        private static void InitialiseGame(Skyscraper skyscraper, Level level)
        {
            level.state = LevelState.Active;

            var gameTime = new TimeSpan();

            while (level.state == LevelState.Active)
            {
                Update(gameTime, skyscraper, level);

                Render(gameTime, skyscraper, level);
            }
        }

        private static void Update(TimeSpan gameTime, Skyscraper skyscraper, Level level)
        {
            gameTime = gameTime.Add(new TimeSpan(0, 0, 0, 0, 1));

            GeneratePassengers(gameTime, skyscraper, level);

            skyscraper.Update(gameTime);

            if (level.WinCriteriaMet(skyscraper))
            {
                level.state = LevelState.Success;
            }
            else
            {
                if (gameTime.TotalSeconds > level.TimeLimit)
                {
                    level.state = LevelState.Failure;
                }
            }
            }

        private static void GeneratePassengers(TimeSpan gameTime, Skyscraper skyscraper, Level level)
        {
            foreach (var arrivalRate in level.PassengerArrivalProfile.FloorArrivalRate)
            {
                if (gameTime >= arrivalRate.LastArrival.Add(new TimeSpan(0,0,0,0, arrivalRate.msPerArrival)))
                {
                    arrivalRate.LastArrival = gameTime;
                    skyscraper.Floors[arrivalRate.Floor].PassengerArrives(skyscraper, new Passenger(2));
                }
            }
        }

        private static void Render(TimeSpan gameTime, Skyscraper skyscraper, Level level)
        {
            //System.Console.WriteLine(skyscraper.Floors[0].PassengersWaiting.Count);
            if (gameTime.Milliseconds == 0 && gameTime.Seconds == 0 && gameTime.Minutes == 0)
            {
                System.Console.WriteLine(gameTime);
                System.Console.WriteLine(skyscraper.Elevators[0].CurrentFloor + " - " + skyscraper.Elevators[0].HeightAboveCurrentFloor);
            }

            switch (level.state)
            {
                case LevelState.Active:
                    break;
                case LevelState.Success:
                    Win(skyscraper, level, gameTime);
                    break;
                case LevelState.Failure:
                    Lose(skyscraper, level, gameTime);
                    break;
                default:
                    break;
            }
        }

        private static void Lose(Skyscraper skyscraper, Level level, TimeSpan gametime)
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

        private static void Win(Skyscraper skyscraper, Level level, TimeSpan gametime)
        {
            System.Console.WriteLine("You Win:" + level.Name + ":" + gametime);
            foreach (var elevator in skyscraper.Elevators)
            {
                System.Console.WriteLine(elevator.UnloadedCount);
            }

            System.Console.WriteLine("Press Any Key To Close:");
            System.Console.ReadLine();
        }
    }
}