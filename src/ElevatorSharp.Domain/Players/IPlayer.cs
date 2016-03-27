using System.Collections.Generic;

namespace ElevatorSharp.Domain.Players
{
    public interface IPlayer
    {
        /// <summary>
        /// Do stuff with the elevators and floors. Handle events on these objects.
        /// </summary>
        /// <param name="elevators"></param>
        /// <param name="floors"></param>
        void Init(IList<IElevator> elevators, IList<IFloor> floors);

        /// <summary>
        /// We normally don't need to do anything here
        /// </summary>
        /// <param name="elevators"></param>
        /// <param name="floors"></param>
        void Update(IList<IElevator> elevators, IList<IFloor> floors);
    }
}