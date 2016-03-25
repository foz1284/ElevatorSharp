using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using ElevatorSharp.Domain;
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
        /// Use ElevatorCommands for sending back to client.
        /// </summary>
        /// <param name="elevatorDto"></param>
        /// <param name="skyscraper"></param>
        /// <returns></returns>
        protected static ElevatorCommands CreateElevatorCommands(ElevatorDto elevatorDto, Skyscraper skyscraper)
        {
            var elevatorCommands = new ElevatorCommands { ElevatorIndex = elevatorDto.ElevatorIndex };
            var destinationQueue = skyscraper.Elevators[elevatorDto.ElevatorIndex].DestinationQueue;
            while (destinationQueue.Count > 0)
            {
                var destination = destinationQueue.Dequeue();

                // TODO: does not take JumpQueue into account
                elevatorCommands.GoToFloor.Enqueue(new GoToFloorCommand(destination, false));
            }
            return elevatorCommands;
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

        /// <summary>
        /// Use ElevatorCommands for sending back to client.
        /// </summary>
        /// <param name="floorDto"></param>
        /// <param name="skyscraper"></param>
        /// <returns></returns>
        protected static ElevatorCommands CreateElevatorCommands(FloorDto floorDto, Skyscraper skyscraper)
        {
            var elevatorCommands = new ElevatorCommands();

            /* First draft: assuming the destinationQueues of each elevator only contain new destinations (not sure that will work),
                then loop through each elevator and create a GoToFloor command.
                TODO: I think we need to add the Elevator index to commands now...
            */ 
            foreach (var elevator in skyscraper.Elevators)
            {
                if (elevator.DestinationQueue.Count > 0)
                {
                    var destination = elevator.DestinationQueue.Dequeue();

                    // TODO: does not take JumpQueue into account
                    elevatorCommands.GoToFloor.Enqueue(new GoToFloorCommand(destination, false));
                }
            }

            return elevatorCommands;
        }
        #endregion
    }
}