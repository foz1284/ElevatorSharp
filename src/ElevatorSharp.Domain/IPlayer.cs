using System.Collections.Generic;

namespace ElevatorSharp.Domain
{
    public interface IPlayer
    {
        /// <summary>
        /// Called every time the elevator stops.
        /// </summary>
        /// <param name="skyscraper"></param>
        void Update(IList<Elevator> elevators, IList<Floor> floors);
    }
}