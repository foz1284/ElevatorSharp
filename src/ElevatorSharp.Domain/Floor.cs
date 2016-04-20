using System;
using System.Collections.Generic;
using ElevatorSharp.Game;
using System.Linq;

namespace ElevatorSharp.Domain
{
    internal class Floor : IFloor
    {
        #region Events
        /// <summary>
        /// Triggered when someone has pressed the up button at a floor. 
        /// Note that passengers will press the button again if they fail to enter an elevator.
        /// Maybe tell an elevator to go to this floor?
        /// </summary>
        public event EventHandler<IElevator[]> UpButtonPressed;

        /// <summary>
        /// Triggered when someone has pressed the down button at a floor. 
        /// Note that passengers will press the button again if they fail to enter an elevator.
        /// Maybe tell an elevator to go to this floor?
        /// </summary>
        public event EventHandler<IElevator[]> DownButtonPressed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the floor number of the floor object.
        /// </summary>
        public int FloorNum { get; }

        public List<Passenger> PassengersWaiting { get; }
        internal void PassengerArrives(Skyscraper skyScraper, Passenger passenger)
        {
            PassengersWaiting.Add(passenger);

            PassengerPressesButton(skyScraper, passenger);
        }

        public void PassengerPressesButton(Skyscraper skyScraper, Passenger passenger)
        {
            if (passenger.DestinationFloor > FloorNum)
            {
                OnUpButtonPressed(skyScraper.Elevators);
            }
            else if (passenger.DestinationFloor < FloorNum)
            {
                OnDownButtonPressed(skyScraper.Elevators);
            }
            else
            { }
        }
        #endregion

        #region Constructors
        internal Floor(int floorNum)
        {
            FloorNum = floorNum;
            PassengersWaiting = new List<Passenger>();
        }
        #endregion
        public bool UpButtonActive = false;
        public bool DownButtonActive = false;
        #region Event Invocators
        public void OnUpButtonPressed(IList<Elevator> e)
        {
            if (!UpButtonActive)
            {
                UpButtonPressed?.Invoke(this, e.ToArray());
                UpButtonActive = true;
            }            
        }

        public void OnDownButtonPressed(IList<Elevator> e)
        {
            if (!DownButtonActive)
            {
                DownButtonPressed?.Invoke(this, e.ToArray());
                DownButtonActive = true;
            }
        } 
        #endregion
    }
}