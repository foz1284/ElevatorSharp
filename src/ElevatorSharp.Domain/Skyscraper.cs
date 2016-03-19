using System.Collections.Generic;

namespace ElevatorSharp.Domain
{
    public class Skyscraper
    {
        #region Fields
        #endregion

        #region Properties
        public IList<Elevator> Elevators { get; }
        public IList<Floor> Floors { get; set; }
        #endregion

        #region Constructors
        public Skyscraper(int elevatorCount, int floorCount, int maxPassengerCount)
        {
            Elevators = CreateElevators(elevatorCount, maxPassengerCount);
            Floors = CreateFloors(floorCount);
        }
        #endregion

        #region Methods
        private static List<Elevator> CreateElevators(int elevatorCount, int maxPassengerCount)
        {
            var elevators = new List<Elevator>();
            for (var i = 0; i < elevatorCount; i++)
            {
                var elevator = new Elevator(maxPassengerCount);
                elevators.Add(elevator);
            }
            return elevators;
        }
        private static List<Floor> CreateFloors(int floorCount)
        {
            var floors = new List<Floor>();
            for (var j = 0; j < floorCount; j++)
            {
                var floor = new Floor(j);
                floors.Add(floor);
            }
            return floors;
        }
        #endregion
    }
}