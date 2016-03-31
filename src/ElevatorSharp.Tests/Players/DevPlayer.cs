using System;
using System.Collections.Generic;
using System.Linq;
using ElevatorSharp.Game;
using ElevatorSharp.Game.Players;

namespace ElevatorSharp.Tests.Players
{
    public class DevPlayer : IPlayer
    {
        #region Implementation of IPlayer
        public void Init(IElevator[] elevators, IFloor[] floors)
        {
            foreach (var elevator in elevators)
            {
                //elevator.Idle += Elevator_Idle; ;
                elevator.FloorButtonPressed += Elevator_FloorButtonPressed;
                //elevator.StoppedAtFloor += Elevator_StoppedAtFloor;
                elevator.PassingFloor += Elevator_PassingFloor;
            }

            foreach (var floor in floors)
            {
                floor.UpButtonPressed += Floor_UpButtonPressed;
                floor.DownButtonPressed += Floor_DownButtonPressed;
            }
        }

        public void Update(IElevator[] elevators, IFloor[] floors)
        {
            // We normally don't need to do anything here
        }
        #endregion

        #region Event Handlers
        private void Elevator_PassingFloor(object sender, PassingFloorEventArgs e)
        {
            var elevator = (IElevator)sender;

            if (e.PassingFloorNumber == 1 && e.Direction == ElevatorDirection.Down && elevator.DestinationDirection != ElevatorDirection.Stopped)
            {
                elevator.Stop(); 
            }

            //var pressedFloors = elevator.PressedFloors;
            //if (pressedFloors != null && pressedFloors.Contains(e.PassingFloorNumber))
            //{
            //    // Stop at this floor next
            //    elevator.GoToFloor(e.PassingFloorNumber, true);
            //}

            //if (e.Direction == ElevatorDirection.Up)
            //{
            //    elevator.GoingUpIndicator = true;
            //    elevator.GoingDownIndicator = false;
            //}
            //else
            //{
            //    elevator.GoingUpIndicator = false;
            //    elevator.GoingDownIndicator = true;
            //}
        }

        private void Floor_DownButtonPressed(object sender, IList<IElevator> elevators)
        {
            var floor = (IFloor)sender;
            
            GoToFloorWithRandomElevator(elevators, floor);
        }

        private void Floor_UpButtonPressed(object sender, IList<IElevator> elevators)
        {
            var floor = (IFloor)sender;

            GoToFloorWithRandomElevator(elevators, floor);
        }

        private void Elevator_Idle(object sender, EventArgs e)
        {
            var elevator = (IElevator)sender;

            elevator.GoToFloor(0);
            elevator.GoToFloor(1);
            elevator.GoToFloor(2);
            elevator.GoToFloor(3);
            elevator.GoToFloor(4);
            elevator.GoToFloor(5);
            elevator.GoToFloor(6);
            elevator.GoToFloor(7);
        }

        private void Elevator_FloorButtonPressed(object sender, int e)
        {
            var elevator = (IElevator)sender;

            // If the elevator is full, then drop off some dudes first by jumping the queue
            if (elevator.LoadFactor > 0.6M)
            {
                elevator.GoToFloor(e, true); // jump queue
            }

            // Check if we're not already going to that floor
            if (!elevator.DestinationQueue.Contains(e))
            {
                // Go to the floor this passenger wants to go
                elevator.GoToFloor(e);
            }
        }

        private void Elevator_StoppedAtFloor(object sender, int e)
        {
            var elevator = (IElevator)sender;

            // TODO: Do something here?
            // A passenger will only get on if the
            // indicator was pointing in the direction in which
            // they want to travel.
        }
        #endregion

        private static void GoToFloorWithRandomElevator(IList<IElevator> elevators, IFloor floor)
        {
            var random = new Random();
            var randomElevatorIndex = random.Next(elevators.Count);
            var elevator = elevators[randomElevatorIndex];
            if (elevator != null && !elevator.DestinationQueue.Contains(floor.FloorNum))
            {
                elevator.GoToFloor(floor.FloorNum);
            }
        }
    }
}