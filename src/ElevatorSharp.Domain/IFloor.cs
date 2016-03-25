using System;
using System.Collections.Generic;

namespace ElevatorSharp.Domain
{
    public interface IFloor
    {
        /// <summary>
        /// Triggered when someone has pressed the up button at a floor. 
        /// Note that passengers will press the button again if they fail to enter an elevator.
        /// Maybe tell an elevator to go to this floor?
        /// </summary>
        event EventHandler<IList<Elevator>> UpButtonPressed;

        /// <summary>
        /// Triggered when someone has pressed the down button at a floor. 
        /// Note that passengers will press the button again if they fail to enter an elevator.
        /// Maybe tell an elevator to go to this floor?
        /// </summary>
        event EventHandler DownButtonPressed;

        /// <summary>
        /// Gets the floor number of the floor object.
        /// </summary>
        int FloorNum { get; }
    }
}