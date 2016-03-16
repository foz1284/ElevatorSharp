using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElevatorSharp.Web.ViewModels;

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
    }
}