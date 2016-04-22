﻿using System;
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
            var gameTime = new TimeSpan();

            while (gameTime.TotalSeconds < level.TimeLimit)
            {
                gameTime = gameTime.Add(new TimeSpan(0, 0, 0, 0, 1));

                Update(gameTime, skyscraper, level);

                Render(gameTime, skyscraper);

                if (level.WinCriteriaMet(skyscraper))
                {
                    Win(skyscraper, level, gameTime);
                    break;
                }
            }

            if (!level.WinCriteriaMet(skyscraper))
            {
                Lose(skyscraper, level, gameTime);
            }
        }

        private static void Update(TimeSpan gameTime, Skyscraper skyscraper, Level level)
        {
            GeneratePassengers(gameTime, skyscraper, level);

            skyscraper.Update(gameTime);
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

        private static void Render(TimeSpan gametime, Skyscraper skyscraper)
        {
            //System.Console.WriteLine(skyscraper.Floors[0].PassengersWaiting.Count);
            if (gametime.Milliseconds == 0 && gametime.Seconds == 0 && gametime.Minutes == 0)
            {
                System.Console.WriteLine(gametime);
                System.Console.WriteLine(skyscraper.Elevators[0].CurrentFloor + " - " + skyscraper.Elevators[0].HeightAboveCurrentFloor);
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