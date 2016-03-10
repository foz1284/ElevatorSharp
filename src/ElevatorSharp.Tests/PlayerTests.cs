﻿using System.Collections.Generic;
using System.Linq;
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

        [Test]
        public void Init_takes_IEnumerable_of_Elevator()
        {
            IPlayer player = new TestPlayer();
            var parameters = player.GetType().GetMethod("Init").GetParameters();
            Assert.IsTrue(parameters.Any(p => p.ParameterType == typeof(IEnumerable<Elevator>)));
        }

        [Test]
        public void Init_takes_IEnumerable_of_Floor()
        {
            IPlayer player = new TestPlayer();
            var parameters = player.GetType().GetMethod("Init").GetParameters();
            Assert.IsTrue(parameters.Any(p => p.ParameterType == typeof(IEnumerable<Floor>)));
        }
    }

    // TODO: Move to ElevatorSharp.Core
    public class Floor
    {
    }

    public class Elevator
    {
    }

    public class TestPlayer : IPlayer
    {
        public void Init(IEnumerable<Elevator> elevators, IEnumerable<Floor> floors)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IPlayer
    {
        void Init(IEnumerable<Elevator> elevators, IEnumerable<Floor> floors);
    }
}