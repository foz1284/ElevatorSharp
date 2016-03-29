using System;
using System.Collections.Generic;
using ElevatorSharp.Game;

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
        #endregion

        #region Constructors
        internal Floor(int floorNum)
        {
            FloorNum = floorNum;
        }
        #endregion

        #region Event Invocators
        public void OnUpButtonPressed(IElevator[] e)
        {
            UpButtonPressed?.Invoke(this, e);
        }

        public void OnDownButtonPressed(IElevator[] e)
        {
            DownButtonPressed?.Invoke(this, e);
        } 
        #endregion
    }
}