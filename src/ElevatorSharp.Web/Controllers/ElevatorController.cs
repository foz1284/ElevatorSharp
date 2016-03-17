using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Index()
        {
            var viewModel = new ElevatorIndexViewModel
            {
                Player = "Test Player",
                Title = "Elevator Sharp"
            };
            return View(viewModel);
        }

        public ContentResult Update(int currentFloor)
        {
            /* TODO:
             * load player
             * load elevator or world or skyscraper?
             * elevator.Move(player)                
            */
            var elevator = new Elevator();
            elevator.GoToFloor(currentFloor); 

            var json = JsonConvert.SerializeObject(elevator);
            Thread.Sleep(100);
            return Content(json, "application/json");
        }
    }
}