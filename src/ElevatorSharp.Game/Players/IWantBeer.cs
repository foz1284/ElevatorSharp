using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorSharp.Game.Players
{
    public class IWantBeer : IPlayer
    {
        public void Init(IElevator[] elevators, IFloor[] floors)
        {
            foreach (var elevator in elevators)
            {
                elevator.Idle += Elevator_Idle; ;
                elevator.FloorButtonPressed += Elevator_FloorButtonPressed;
                elevator.StoppedAtFloor += Elevator_StoppedAtFloor;
                //elevator.PassingFloor += Elevator_PassingFloor;
            }

            foreach (var floor in floors)
            {
                floor.UpButtonPressed += Floor_UpButtonPressed;
                floor.DownButtonPressed += Floor_DownButtonPressed;
            }
        }

        #region Event Handlers
        private void Elevator_PassingFloor(object sender, PassingFloorEventArgs e)
        {
            var elevator = (IElevator)sender;

            //if (e.PassingFloorNumber == 1 && e.Direction == ElevatorDirection.Down && elevator.DestinationDirection != ElevatorDirection.Stopped)
            //{
            //    elevator.Stop(); 
            //}

            var pressedFloors = elevator.PressedFloors;
            if (pressedFloors != null && pressedFloors.Contains(e.PassingFloorNumber))
            {
                // Stop at this floor next
                elevator.GoToFloor(e.PassingFloorNumber, true);
            }

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

            //var destination = floor.FloorNum;

            //var distance = 99;
            //var chosenElevatorIndex = 0;
            //for (var index = 0; index < elevators.Count; index++)
            //{
            //    var elevator = elevators[index];
            //    var newDistance = Math.Abs(destination - elevator.CurrentFloor);
            //    if (newDistance < distance)
            //    {
            //        distance = newDistance;
            //        chosenElevatorIndex = index;
            //    }
            //}

            //elevators[chosenElevatorIndex].GoToFloor(destination, true);
            GoToFloorWithRandomElevator(elevators, floor);
        }

        private void Floor_UpButtonPressed(object sender, IList<IElevator> elevators)
        {
            var floor = (IFloor)sender;

            //var destination = floor.FloorNum;

            //var distance = 99;
            //var chosenElevatorIndex = 0;
            //for (var index = 0; index < elevators.Count; index++)
            //{
            //    var elevator = elevators[index];
            //    var newDistance = Math.Abs(destination - elevator.CurrentFloor);
            //    if (newDistance < distance)
            //    {
            //        distance = newDistance;
            //        chosenElevatorIndex = index;
            //    }
            //}

            //elevators[chosenElevatorIndex].GoToFloor(destination, true);
            GoToFloorWithRandomElevator(elevators, floor);
        }

        private void Elevator_Idle(object sender, EventArgs e)
        {
            var elevator = (IElevator)sender;
            elevator.GoToFloor(0);
            //foreach (var pressedFloor in elevator.PressedFloors)
            //{
            //    elevator.GoToFloor(pressedFloor);
            //}
        }

        private void Elevator_FloorButtonPressed(object sender, int e)
        {
            var elevator = (IElevator)sender;

            if (elevator == null) return;

            // If the elevator is full, then drop off some dudes first by jumping the queue
            if (elevator.LoadFactor > 0.6M)
            {
                elevator.GoToFloor(e, true); // jump queue
            }

            // Check if we're not already going to that floor
            if (elevator.DestinationQueue != null && !elevator.DestinationQueue.Contains(e))
            {
                // Go to the floor this passenger wants to go
                elevator.GoToFloor(e, true);
            }
        }

        private void Elevator_StoppedAtFloor(object sender, int e)
        {
            var elevator = (IElevator)sender;

            if (elevator.DestinationQueue != null && elevator.DestinationQueue.Contains(e))
            {
                var destinationQueue = elevator.DestinationQueue.ToList();
                destinationQueue.Remove(e);
                var newQueue = new Queue<int>();
                foreach (var i in destinationQueue)
                {
                    newQueue.Enqueue(i);
                }
                elevator.DestinationQueue = newQueue;
            }
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