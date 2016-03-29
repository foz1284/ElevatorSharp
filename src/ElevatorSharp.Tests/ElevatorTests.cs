using ElevatorSharp.Domain;
using ElevatorSharp.Game;
using NUnit.Framework;

namespace ElevatorSharp.Tests
{
    [TestFixture]
    public class ElevatorTests
    {
        [Test]
        public void GoToFloor_queues_elevator_to_go_to_specified_floor_number()
        {
            // Arrange
            IElevator elevator = new Elevator(0);

            // Act
            elevator.GoToFloor(0);

            // Assert
            Assert.AreEqual(1, elevator.DestinationQueue.Count);
        }
    }
}