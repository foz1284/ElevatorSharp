using System.Collections.Generic;
using ElevatorSharp.Domain.Players;

namespace ElevatorSharp.Domain
{
    public class Skyscraper
    {
        #region Fields
        #endregion

        #region Properties
        public IList<Elevator> Elevators { get; }
        public IList<Floor> Floors { get; }
        #endregion

        #region Constructors
        public Skyscraper(int elevatorCount, int floorCount, int maxPassengerCount)
        {
            Elevators = CreateElevators(elevatorCount, maxPassengerCount);
            Floors = CreateFloors(floorCount);
        }
        #endregion

        #region Methods
        public List<Elevator> CreateElevators(int elevatorCount, int maxPassengerCount)
        {
            var elevators = new List<Elevator>();
            for (var i = 0; i < elevatorCount; i++)
            {
                var elevator = new Elevator(maxPassengerCount);
                elevators.Add(elevator);
            }
            return elevators;
        }

        public List<Floor> CreateFloors(int floorCount)
        {
            var floors = new List<Floor>();
            for (var j = 0; j < floorCount; j++)
            {
                var floor = new Floor(j);
                floors.Add(floor);
            }
            return floors;
        }

        public void LoadPlayer(IPlayer player)
        {
            player.Init(Elevators, Floors);
        }
        #endregion
    }
}