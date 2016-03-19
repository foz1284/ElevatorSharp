using System.Collections.Generic;

namespace ElevatorSharp.Domain
{
    public interface IPlayer
    {
        void Init(IList<Elevator> elevators, IList<Floor> floors);

        /// <summary>
        /// Called every time the elevator stops.
        /// </summary>
        /// <param name="skyscraper"></param>
        void Update(IList<Elevator> elevators, IList<Floor> floors);
    }
}