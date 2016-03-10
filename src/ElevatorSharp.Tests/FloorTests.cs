using ElevatorSharp.Domain;
using NUnit.Framework;

namespace ElevatorSharp.Tests
{
    [TestFixture]
    public class FloorTests
    {
        [Test]
        public void Level_returns_level_of_floor()
        {
            // Arrange
            IFloor floor = new Floor(1);

            // Assert
            Assert.AreEqual(1, floor.Level);
        }
         
    }
}