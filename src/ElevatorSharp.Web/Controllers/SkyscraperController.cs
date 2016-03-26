using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using ElevatorSharp.Domain;
using ElevatorSharp.Domain.Players;
using ElevatorSharp.Web.DataTransferObjects;
using ElevatorSharp.Web.ViewModels;
using Newtonsoft.Json;

namespace ElevatorSharp.Web.Controllers
{
    public class SkyscraperController : Controller
    {
        public ActionResult Index(string message = null)
        {
            var viewModel = new SkyscraperIndexViewModel
            {
                Player = "Test Player",
                Title = "Elevator Sharp"
            };

            ViewBag.Message = message;
            return View("Original", viewModel);
        }

        public ActionResult UploadPlayer(HttpPostedFileBase dll)
        {
            var message = "Could not load assembly";
            if (dll == null || dll.ContentLength <= 0) return RedirectToAction("Index", new {message});

            // Load player dll
            var fileName = Path.GetFileName(dll.FileName);
            var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
            dll.SaveAs(path);
            var playerAssembly = Assembly.LoadFile(path);
            foreach (var type in playerAssembly.GetTypes().OrderBy(t => t.Name))
            {
                if (type.GetInterface("IPlayer") != null)
                {
                    var player = Activator.CreateInstance(type) as IPlayer;
                    SavePlayer((IPlayer)player);
                    //SavePlayerName(type.Name);
                    message = type.Name + " uploaded.";
                    return RedirectToAction("Index", new { message });
                }
            }
            message = "No player implementing IPlayer found.";
            return RedirectToAction("Index", new { message });
        }

        public ContentResult New(ElevatorDto[] elevators, FloorDto[] floors)
        {
            var skyscraper = new Skyscraper(elevators.Length, floors.Length);
            var player = LoadPlayer();
            skyscraper.LoadPlayer(player); // This calls the Init method on Player and hook up events
            SaveSkyscraper(skyscraper);

            var json = JsonConvert.SerializeObject("Ready."); // TODO: Not needed, unless you want to pass some UI message back.
            return Content(json, "application/json");
        }

        #region Helper Methods
        private static void SaveSkyscraper(Skyscraper skyscraper)
        {
            var cache = MemoryCache.Default;
            cache.Remove("skyscraper");
            cache.Set("skyscraper", skyscraper, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddDays(1) });
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
            return new DevPlayer();
        }
        #endregion  
    }
}