using System.Collections.Generic;

namespace ElevatorSharp.Domain
{
    public interface IPlayer
    {
        void Init(IEnumerable<Elevator> elevators, IEnumerable<Floor> floors);
    }
}