using System.Collections.Generic;

namespace ElevatorSharp.Game
{
    public interface IPlayer
    {
        /// <summary>
        /// Do stuff with the elevators and floors. Handle events on these objects.
        /// </summary>
        /// <param name="elevators"></param>
        /// <param name="floors"></param>
        void Init(IElevator[] elevators, IFloor[] floors);

        /// <summary>
        /// We normally don't need to do anything here
        /// </summary>
        /// <param name="elevators"></param>
        /// <param name="floors"></param>
        void Update(IElevator[] elevators, IFloor[] floors);
    }
}