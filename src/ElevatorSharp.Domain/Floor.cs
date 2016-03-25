using System;
using System.Collections.Generic;

namespace ElevatorSharp.Domain
{
    public class Floor : IFloor
    {
        #region Events
        /// <summary>
        /// Triggered when someone has pressed the up button at a floor. 
        /// Note that passengers will press the button again if they fail to enter an elevator.
        /// Maybe tell an elevator to go to this floor?
        /// </summary>
        public event EventHandler<IList<Elevator>> UpButtonPressed;

        /// <summary>
        /// Triggered when someone has pressed the down button at a floor. 
        /// Note that passengers will press the button again if they fail to enter an elevator.
        /// Maybe tell an elevator to go to this floor?
        /// </summary>
        public event EventHandler DownButtonPressed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the floor number of the floor object.
        /// </summary>
        public int FloorNum { get; }
        #endregion

        #region Constructors
        public Floor(int floorNum)
        {
            FloorNum = floorNum;
        }
        #endregion

        #region Event Invocators
        protected virtual void OnUpButtonPressed(IList<Elevator> e)
        {
            UpButtonPressed?.Invoke(this, e);
        }

        protected virtual void OnDownButtonPressed()
        {
            DownButtonPressed?.Invoke(this, EventArgs.Empty);
        } 
        #endregion

        
    }
}