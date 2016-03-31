using System.Linq;
using ElevatorSharp.Game.Tests.Mocks;
using NUnit.Framework;

namespace ElevatorSharp.Game.Tests
{
    [TestFixture]
    public class ElevatorPlayerTests
    {
        [Test]
        public void PlayerSendsElevatorToFloorWhereElevatorButtonIsPressed()
        {
            // Arrange
            IPlayer player = null; // new YourPlayer();
            var elevator = new MockElevator();
            var elevators = new IElevator[] { elevator };
            var floors = new IFloor[] {new MockFloor(1), new MockFloor(2) };
            player.Init(elevators, floors);

            // Act
            elevator.TestInvokeFloorButtonPressedEvent(2);

            // Assert
            Assert.True(elevator.CalledMethods.Contains("GoToFloor(2)"));
        }

        [Test]
        public void PlayerSendsElevatorToFloorWhereFloorButtonIsPressed()
        {
            // Arrange
            IPlayer player = null; // new YourPlayer();
            var elevator = new MockElevator();
            var elevators = new IElevator[] { elevator };
            var floors = new IFloor[] { new MockFloor(1), new MockFloor(2) };
            player.Init(elevators, floors);

            // Act
            (floors[1] as MockFloor)?.TestInvokeDownButtonPressedEvent(elevators);

            // Assert
            Assert.True(elevator.CalledMethods.Contains("GoToFloor(2)"));
        }
    }
}
