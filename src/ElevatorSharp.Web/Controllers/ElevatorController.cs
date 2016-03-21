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

        #region Event Actions
        public ContentResult Idle(IdleViewModel viewModel)
        {
            // TODO: Trigger Idle event on Elevator with IElevatorDriver from player

            // First test passing back int for goToFloor call on client
            var floorLevel = 2;

            var json = JsonConvert.SerializeObject(floorLevel);
            return Content(json, "application/json");
        }
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