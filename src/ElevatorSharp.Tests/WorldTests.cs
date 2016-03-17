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
            var skyscraper = new Skyscraper();

            // Act
            var floors = skyscraper.CreateFloors(0);

            // Assert
            Assert.AreEqual(typeof(List<Floor>), floors.GetType());
        }

        [Test]
        public void CreateFloors_returns_List_of_Floor_equal_to_floorCount()
        {
            // Arrange
            var skyscraper = new Skyscraper();

            // Act
            var floors = skyscraper.CreateFloors(5);

            // Assert
            Assert.AreEqual(5, floors.Count);
        }

        public void CreateElevators_returns_List_of_Elevators()
        {
            // Arrange
            var skyscraper = new Skyscraper();

            // Act
            var elevators = skyscraper.CreateElevators();

            // Assert
            Assert.AreEqual(typeof(List<Elevator>), elevators.GetType());
        }

    }
}