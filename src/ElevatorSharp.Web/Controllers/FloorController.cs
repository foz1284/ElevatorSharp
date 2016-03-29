using System.Linq;
using System.Web.Mvc;
using ElevatorSharp.Domain.DataTransferObjects;
using ElevatorSharp.Game;
using ElevatorSharp.Web.ViewModels;
using Newtonsoft.Json;

namespace ElevatorSharp.Web.Controllers
{
    public class FloorController : BaseController
    {
        #region Floor Events
        /// <summary>
        /// Triggered when someone has pressed the up button at a floor. 
        /// Note that passengers will press the button again if they fail to enter an elevator.
        /// Maybe tell an elevator to go to this floor?
        /// </summary>
        /// <param name="skyscraperDto"></param>
        /// <returns></returns>
        public ContentResult UpButtonPressed(SkyscraperDto skyscraperDto)
        {
            var skyscraper = SyncSkyscraper(skyscraperDto);
            var floor = skyscraper.Floors[skyscraperDto.EventRaisedFloorNumber];
            var elevators = skyscraper.Elevators;
            foreach (var elevator in elevators)
            {
                // We use these two queues to keep track of new destinations so that we can create elevator commands
                elevator.NewDestinations.Clear();
                elevator.JumpQueueDestinations.Clear();
            }

            // This invokes the delegate from IPlayer
            floor.OnUpButtonPressed(elevators.ToArray());

            var elevatorCommands = CreateElevatorCommands(skyscraperDto, skyscraper);
            var json = JsonConvert.SerializeObject(elevatorCommands);
            return Content(json, "application/json");
        }

        /// <summary>
        /// Triggered when someone has pressed the down button at a floor. 
        /// Note that passengers will press the button again if they fail to enter an elevator.
        /// Maybe tell an elevator to go to this floor?
        /// </summary>
        /// <param name="skyscraperDto"></param>
        /// <returns></returns>
        public ContentResult DownButtonPressed(SkyscraperDto skyscraperDto)
        {
            var skyscraper = SyncSkyscraper(skyscraperDto);
            var floor = skyscraper.Floors[skyscraperDto.EventRaisedFloorNumber];
            var elevators = skyscraper.Elevators;
            foreach (var elevator in elevators)
            {
                // We use these two queues to keep track of new destinations so that we can create elevator commands
                elevator.NewDestinations.Clear();
                elevator.JumpQueueDestinations.Clear();
            }

            // This invokes the delegate from IPlayer
            floor.OnDownButtonPressed(elevators.ToArray());

            var elevatorCommands = CreateElevatorCommands(skyscraperDto, skyscraper);
            var json = JsonConvert.SerializeObject(elevatorCommands);
            return Content(json, "application/json");
        }
        #endregion
    }
}