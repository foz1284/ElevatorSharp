using System;
using System.Collections.Generic;

namespace ElevatorSharp.Game.Tests.Mocks
{
    public class MockFloor : IFloor
    {
        public MockFloor(int floorNum)
        {
            FloorNum = floorNum;
        }

        public int FloorNum { get; }
        public event EventHandler<IElevator[]> UpButtonPressed;
        public event EventHandler<IElevator[]> DownButtonPressed;

        #region Test Helpers

        public void TestInvokeUpButtonPressedEvent(IElevator[] elevators)
        {
            UpButtonPressed?.Invoke(this, elevators);
        }

        public void TestInvokeDownButtonPressedEvent(IElevator[] elevators)
        {
            DownButtonPressed?.Invoke(this, elevators);
        }

        #endregion
    }
}