using System.Linq;
using ElevatorSharp.Game.Players;
using ElevatorSharp.Game.Tests.Mocks;
using NUnit.Framework;

namespace ElevatorSharp.Game.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        private IPlayer yourPlayer;

        [SetUp]
        public void RunBeforeEachTest()
        {
            yourPlayer = new BootstrapPlayer(); // new YourPlayer();
        }

        [Test]
        public void PlayerSendsElevatorToFloorWhereElevatorButtonIsPressed()
        {
            // Arrange
            var elevator = new MockElevator();
            var elevators = new IElevator[] { elevator };
            var floors = new IFloor[] {new MockFloor(1), new MockFloor(2) };
            yourPlayer.Init(elevators, floors);

            // Act
            elevator.TestInvokeFloorButtonPressedEvent(2);

            // Assert
            Assert.True(elevator.CalledMethods.Contains("GoToFloor(2)"));
        }

        [Test]
        public void PlayerSendsElevatorToFloorWhereFloorButtonIsPressed()
        {
            // Arrange
            var elevator = new MockElevator();
            var elevators = new IElevator[] { elevator };
            var floors = new IFloor[] { new MockFloor(1), new MockFloor(2) };
            yourPlayer.Init(elevators, floors);

            // Act
            (floors[1] as MockFloor)?.TestInvokeDownButtonPressedEvent(elevators);

            // Assert
            Assert.True(elevator.CalledMethods.Contains("GoToFloor(2)"));
        }
    }
}
