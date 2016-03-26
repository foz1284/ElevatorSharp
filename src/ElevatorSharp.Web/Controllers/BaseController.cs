using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using ElevatorSharp.Domain;
using ElevatorSharp.Domain.DataTransferObjects;
using ElevatorSharp.Web.ViewModels;

namespace ElevatorSharp.Web.Controllers
{
    public class BaseController : Controller
    {
        #region Helper Methods
        private static Skyscraper LoadSkyscraper()
        {
            var cache = MemoryCache.Default;
            if (cache.Contains("skyscraper"))
            {
                return (Skyscraper)cache.Get("skyscraper");
            }
            return null;
        }

        /// <summary>
        /// Transfer all required data from client.
        /// </summary>
        /// <param name="elevatorDto"></param>
        /// <returns></returns>
        protected static Skyscraper SyncSkyscraper(ElevatorDto elevatorDto)
        {
            var skyscraper = LoadSkyscraper();
            skyscraper.Elevators[elevatorDto.ElevatorIndex].PressedFloors = elevatorDto.PressedFloors;

            skyscraper.Elevators[elevatorDto.ElevatorIndex].DestinationQueue.Clear();
            if (elevatorDto.DestinationQueue != null)
            {
                foreach (var floor in elevatorDto.DestinationQueue)
                    skyscraper.Elevators[elevatorDto.ElevatorIndex].DestinationQueue.Enqueue(floor);
            }

            return skyscraper;
        }

        /// <summary>
        /// Transfer all required data from client.
        /// </summary>
        /// <param name="floorDto"></param>
        /// <returns></returns>
        protected static Skyscraper SyncSkyscraper(FloorDto floorDto)
        {
            var skyscraper = LoadSkyscraper();

            // TODO: What do we need to sync here? Elevators?

            return skyscraper;
        }

        // Trying to abstract SyncSkyscraper so we don't need two overloads for different dtos
        protected static Skyscraper SyncSkyscraper(SkyscraperDto skyscraperDto)
        {
            var skyscraper = LoadSkyscraper();
            for (var i = 0; i < skyscraperDto.Elevators.Length; i++)
            {
                skyscraper.Elevators[i].PressedFloors = skyscraperDto.Elevators[i].PressedFloors;
                skyscraper.Elevators[i].DestinationQueue.Clear();
                if (skyscraperDto.Elevators[i].DestinationQueue == null) continue;
                foreach (var floor in skyscraperDto.Elevators[i].DestinationQueue)
                    skyscraper.Elevators[i].DestinationQueue.Enqueue(floor);
            }

            return skyscraper;
        }

        /// <summary>
        /// Use ElevatorCommands for sending back to client.
        /// </summary>
        /// <param name="skyscraperDto"></param>
        /// <param name="skyscraper"></param>
        /// <returns></returns>
        protected static ElevatorCommands CreateElevatorCommands(SkyscraperDto skyscraperDto, Skyscraper skyscraper)
        {
            var elevatorCommands = new ElevatorCommands();
            var jumpQueueDestinations = skyscraper.Elevators[skyscraperDto.EventRaisedElevatorIndex].JumpQueueDestinations;
            var newDestinations = skyscraper.Elevators[skyscraperDto.EventRaisedElevatorIndex].NewDestinations;

            while (jumpQueueDestinations.Count > 0)
            {
                AddGoToFloorCommands(skyscraperDto, jumpQueueDestinations, elevatorCommands);
            }

            while (newDestinations.Count > 0)
            {
                AddGoToFloorCommands(skyscraperDto, newDestinations, elevatorCommands);
            }
            return elevatorCommands;
        }

        private static void AddGoToFloorCommands(SkyscraperDto skyscraperDto, Queue<int> queue, ElevatorCommands elevatorCommands)
        {
            var destination = queue.Dequeue();
            var goToFloorCommand = new GoToFloorCommand(skyscraperDto.EventRaisedElevatorIndex, destination, true);
            elevatorCommands.GoToFloor.Enqueue(goToFloorCommand);
        }


        /// <summary>
        /// Use ElevatorCommands for sending back to client.
        /// </summary>
        /// <param name="floorDto"></param>
        /// <param name="skyscraper"></param>
        /// <returns></returns>
        protected static ElevatorCommands CreateElevatorCommands(FloorDto floorDto, Skyscraper skyscraper)
        {
            var elevatorCommands = new ElevatorCommands();
            
            foreach (var elevator in skyscraper.Elevators)
            {
                if (elevator.JumpQueueDestinations.Count > 0)
                {
                    var destination = elevator.JumpQueueDestinations.Dequeue();
                    elevatorCommands.GoToFloor.Enqueue(new GoToFloorCommand(elevator.Index, destination, true));
                }

                if (elevator.NewDestinations.Count > 0)
                {
                    var destination = elevator.NewDestinations.Dequeue();
                    elevatorCommands.GoToFloor.Enqueue(new GoToFloorCommand(elevator.Index, destination, false));
                }
            }

            return elevatorCommands;
        }
        #endregion
    }
}