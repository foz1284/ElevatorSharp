using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ElevatorSharp.Domain;
using ElevatorSharp.Web.ViewModels;
using Newtonsoft.Json;

namespace ElevatorSharp.Web.Controllers
{
    public class ElevatorController : Controller
    {
        #region Elevator Events
        /// <summary>
        /// Triggered when the elevator has completed all its tasks and is not doing anything.
        /// </summary>
        /// <param name="elevatorDto"></param>
        /// <returns></returns>
        public ContentResult Idle(ElevatorDto elevatorDto)
        {
            var skyscraper = SyncSkyscraper(elevatorDto);

            // This invokes the delegate from IPlayer
            skyscraper.Elevators[elevatorDto.ElevatorIndex].OnIdle(); 

            var elevatorCommands = CreateElevatorCommands(elevatorDto, skyscraper);
            var json = JsonConvert.SerializeObject(elevatorCommands);
            return Content(json, "application/json");
        }

        /// <summary>
        /// Triggered when a passenger has pressed a button inside the elevator. 
        /// This tells us which floor the passenger wants to go to.
        /// Maybe tell the elevator to go to that floor?
        /// </summary>
        /// <param name="elevatorDto"></param>
        /// <returns></returns>
        public ContentResult FloorButtonPressed(ElevatorDto elevatorDto)
        {
            var skyscraper = SyncSkyscraper(elevatorDto);

            // This invokes the delegate from IPlayer
            skyscraper.Elevators[elevatorDto.ElevatorIndex].OnFloorButtonPressed(elevatorDto.FloorNumberPressed);

            var elevatorCommands = CreateElevatorCommands(elevatorDto, skyscraper);
            var json = JsonConvert.SerializeObject(elevatorCommands);
            return Content(json, "application/json");
        }

        /// <summary>
        /// Triggered slightly before the elevator will pass a floor. 
        /// A good time to decide whether to stop at that floor. 
        /// Note that this event is not triggered for the destination floor. 
        /// Direction is either "up" or "down".
        /// </summary>
        /// <param name="elevatorDto"></param>
        /// <returns></returns>
        public ContentResult PassingFloor(ElevatorDto elevatorDto)
        {
            var json = JsonConvert.SerializeObject(elevatorDto);
            return Content(json, "application/json");
        }

        /// <summary>
        /// Triggered when the elevator has arrived at a floor.
        /// Maybe decide where to go next?
        /// </summary>
        /// <param name="elevatorDto"></param>
        /// <returns></returns>
        public ContentResult StoppedAtFloor(ElevatorDto elevatorDto)
        {
            var json = JsonConvert.SerializeObject(elevatorDto);
            return Content(json, "application/json");
        }
        #endregion

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
        private static Skyscraper SyncSkyscraper(ElevatorDto elevatorDto)
        {
            var skyscraper = LoadSkyscraper();
            skyscraper.Elevators[elevatorDto.ElevatorIndex].PressedFloors = elevatorDto.PressedFloors;
            return skyscraper;
        }

        /// <summary>
        /// Use ElevatorCommands for sending back to client.
        /// </summary>
        /// <param name="elevatorDto"></param>
        /// <param name="skyscraper"></param>
        /// <returns></returns>
        private static ElevatorCommands CreateElevatorCommands(ElevatorDto elevatorDto, Skyscraper skyscraper)
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
        #endregion
    }
}