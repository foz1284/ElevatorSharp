using System.Collections.Generic;

namespace ElevatorSharp.Domain.Players
{
    public interface IPlayer
    {
        void Init(IList<Elevator> elevators, IList<Floor> floors);

        /// <summary>
        /// We normally don't need to do anything here
        /// </summary>
        /// <param name="elevators"></param>
        /// <param name="floors"></param>
        void Update(IList<Elevator> elevators, IList<Floor> floors);
    }
}