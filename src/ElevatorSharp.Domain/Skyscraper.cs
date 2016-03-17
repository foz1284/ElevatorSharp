using System.Collections.Generic;

namespace ElevatorSharp.Domain
{
    public class Skyscraper
    {
        public List<Floor> CreateFloors(int floorCount)
        {
            var floors = new List<Floor>();
            for (var i = 0; i < floorCount; i++)
            {
                floors.Add(new Floor(i));
            }
            return floors;
        }

        public List<Elevator> CreateElevators()
        {
            return new List<Elevator>();
        }
    }
}