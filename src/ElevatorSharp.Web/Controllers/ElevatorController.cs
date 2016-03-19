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
        #region Actions
        public ActionResult Index()
        {
            var viewModel = new ElevatorIndexViewModel
            {
                Player = "Test Player",
                Title = "Elevator Sharp"
            };
            return View(viewModel);
        }

        public ContentResult Build(int elevators, int floors, int maxPassengers)
        {
            var skyscraper = new Skyscraper(elevators, floors, maxPassengers);

            SaveSkyscraper(skyscraper);

            var json = JsonConvert.SerializeObject(skyscraper);
            return Content(json, "application/json");
        }

        public ContentResult Update(int currentFloor)
        {
            var skyscraper = LoadSkyscraper();
            var player = LoadPlayer();
            //player.Update(skyscraper.Elevators);

            var json = JsonConvert.SerializeObject(skyscraper);
            Thread.Sleep(100);
            return Content(json, "application/json");
        } 
        #endregion

        #region Methods
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