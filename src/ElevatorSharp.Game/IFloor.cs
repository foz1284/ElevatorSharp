using System;

namespace ElevatorSharp.Game
{
    public interface IFloor
    {
        #region Events
        /// <summary>
        /// Triggered when someone has pressed the up button at a floor. 
        /// Note that passengers will press the button again if they fail to enter an elevator.
        /// Maybe tell an elevator to go to this floor?
        /// </summary>
        event EventHandler<IElevator[]> UpButtonPressed;

        /// <summary>
        /// Triggered when someone has pressed the down button at a floor. 
        /// Note that passengers will press the button again if they fail to enter an elevator.
        /// Maybe tell an elevator to go to this floor?
        /// </summary>
        event EventHandler<IElevator[]> DownButtonPressed;
        #endregion

        #region Event Invocators
        void OnUpButtonPressed(IElevator[] e);
        void OnDownButtonPressed(IElevator[] e);
        #endregion

        #region Properties
        /// <summary>
        /// Gets the floor number of the floor object.
        /// </summary>
        int FloorNum { get; } 
        #endregion
    }
}