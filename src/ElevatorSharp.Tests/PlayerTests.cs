using NUnit.Framework;

namespace ElevatorSharp.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void Player_has_init_method()
        {
            IPlayer player = new TestPlayer();
            Assert.IsNotNull(player.GetType().GetMethod("Init"));
        }
         
    }

    public class TestPlayer : IPlayer
    {
        public void Init()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IPlayer
    {
        void Init();
    }
}