using System.Collections.Generic;
using ElevatorSharp.Domain;
using NUnit.Framework;

namespace ElevatorSharp.Tests
{
    [TestFixture]
    public class WorldTests
    {
        [Test]
        public void CreateFloors_returns_List_of_Floor()
        {
            // Arrange
            var world = new World();

            // Act
            var floors = world.CreateFloors(0);

            // Assert
            Assert.AreEqual(typeof(List<Floor>), floors.GetType());
        }

        [Test]
        public void CreateFloors_returns_List_of_Floor_equal_to_floorCount()
        {
            // Arrange
            var world = new World();

            // Act
            var floors = world.CreateFloors(5);

            // Assert
            Assert.AreEqual(5, floors.Count);
        }

        public void CreateElevators_returns_List_of_Elevators()
        {
            // Arrange
            var world = new World();

            // Act
            var elevators = world.CreateElevators();

            // Assert
            Assert.AreEqual(typeof(List<Elevator>), elevators.GetType());
        }

    }

    public class World
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