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
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ContentResult Idle(ElevatorDto viewModel)
        {
            // TODO: How do we know which elevator triggered the event?

            var skyscraper = LoadSkyscraper();
            skyscraper.Elevators[0].PressedFloors = viewModel.PressedFloors;
            skyscraper.Elevators[0].OnIdle(); // Does this invoke the delegate from TestPlayer? ... yes it does!

            // DestinationQueue serialises correctly from client to viewModel!
            // if (viewModel.DestinationQueue == null) viewModel.DestinationQueue = new Queue<int>();
            // viewModel.DestinationQueue.Enqueue(0);
            
            // Using new ElevatorCommands class for returning data
            var elevatorCommands = new ElevatorCommands();
            var destinationQueue = skyscraper.Elevators[0].DestinationQueue;
            while (destinationQueue.Count > 0)
            {
                var destination = destinationQueue.Dequeue();

                // TODO: does not take JumpQueue into account
                elevatorCommands.GoToFloor.Enqueue(new GoToFloorCommand(destination, false));
            }

            var json = JsonConvert.SerializeObject(elevatorCommands);
            return Content(json, "application/json");
        }

        /// <summary>
        /// Triggered when a passenger has pressed a button inside the elevator. 
        /// This tells us which floor the passenger wants to go to.
        /// Maybe tell the elevator to go to that floor?
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ContentResult FloorButtonPressed(FloorButtonPressedViewModel viewModel)
        {
            var json = JsonConvert.SerializeObject(viewModel);
            return Content(json, "application/json");
        }

        /// <summary>
        /// Triggered slightly before the elevator will pass a floor. 
        /// A good time to decide whether to stop at that floor. 
        /// Note that this event is not triggered for the destination floor. 
        /// Direction is either "up" or "down".
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ContentResult PassingFloor(PassingFloorViewModel viewModel)
        {
            var json = JsonConvert.SerializeObject(viewModel);
            return Content(json, "application/json");
        }

        /// <summary>
        /// Triggered when the elevator has arrived at a floor.
        /// Maybe decide where to go next?
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ContentResult StoppedAtFloor(StoppedAtFloorViewModel viewModel)
        {
            var json = JsonConvert.SerializeObject(viewModel);
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
        #endregion
    }
}