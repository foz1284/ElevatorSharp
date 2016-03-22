using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElevatorSharp.Web.ViewModels;
using Newtonsoft.Json;

namespace ElevatorSharp.Web.Controllers
{
    public class FloorController : Controller
    {
        #region Floor Events
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
        #endregion
    }
}