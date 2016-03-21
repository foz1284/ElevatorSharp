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
        #region UI Actions
        public ActionResult Index()
        {
            var viewModel = new ElevatorIndexViewModel
            {
                Player = "Test Player",
                Title = "Elevator Sharp"
            };
            return View("Original", viewModel);
        }
        #endregion

        #region Elevator Events
        /// <summary>
        /// Triggered when the elevator has completed all its tasks and is not doing anything.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ContentResult Idle(IdleViewModel viewModel)
        {
            // DestinationQueue serialises correctly from client to viewModel!
            // if (viewModel.DestinationQueue == null) viewModel.DestinationQueue = new Queue<int>();
            // viewModel.DestinationQueue.Enqueue(0);
            
            // ... but maybe it's easier to have a new GoToFloors queue?
            viewModel.GoToFloors = new Queue<int>();
            viewModel.GoToFloors.Enqueue(2);

            var json = JsonConvert.SerializeObject(viewModel);
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

        /// <summary>
        /// Triggered when someone has pressed the up button at a floor. 
        /// Note that passengers will press the button again if they fail to enter an elevator.
        /// Maybe tell an elevator to go to this floor?
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ContentResult UpButtonPressed(UpButtonPressedViewModel viewModel)
        {
            var json = JsonConvert.SerializeObject(viewModel);
            return Content(json, "application/json");
        }

        /// <summary>
        /// Triggered when someone has pressed the down button at a floor. 
        /// Note that passengers will press the button again if they fail to enter an elevator.
        /// Maybe tell an elevator to go to this floor?
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ContentResult DownButtonPressed(DownButtonPressedViewModel viewModel)
        {
            var json = JsonConvert.SerializeObject(viewModel);
            return Content(json, "application/json");
        }
        #region Floor Events

        #endregion

        #region Helper Methods
        private static void SaveSkyscraper(Skyscraper skyscraper)
        {
            var cache = MemoryCache.Default;
            cache.Set("skyscraper", skyscraper, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddDays(1) });
        }

        private static Skyscraper LoadSkyscraper()
        {
            var cache = MemoryCache.Default;
            if (cache.Contains("skyscraper"))
            {
                return (Skyscraper)cache.Get("skyscraper");
            }
            return null;
        }

        private static void SavePlayer(IPlayer player)
        {
            var cache = MemoryCache.Default;
            cache.Set("player", player, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddDays(1) });
        }

        private static IPlayer LoadPlayer()
        {
            var cache = MemoryCache.Default;
            if (cache.Contains("player"))
            {
                return (IPlayer)cache.Get("player");
            }
            return new TestPlayer();
        }
        #endregion  
    }
}