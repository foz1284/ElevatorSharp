using System;
using System.Collections.Generic;

namespace ElevatorSharp.Game.Tests.Mocks
{
    public class MockElevator : IElevator
    {
        public MockElevator()
        {
            _calledMethods = new List<string>();
        }

        public void GoToFloor(int floor)
        {
            _calledMethods.Add($"GoToFloor({floor})");
        }

        public void GoToFloor(int floor, bool jumpQueue)
        {
            _calledMethods.Add($"GoToFloor({floor}, {jumpQueue})");
        }

        public void Stop()
        {
            _calledMethods.Add("Stop()");
        }

        public void CheckDestinationQueue()
        {
            _calledMethods.Add("CheckDestinationQueue()");
        }

        public int[] PressedFloors { get; }
        public int CurrentFloor { get; }
        public bool GoingUpIndicator { get; set; }
        public bool GoingDownIndicator { get; set; }
        public int MaxPassengerCount { get; }
        public decimal LoadFactor { get; }
        public ElevatorDirection DestinationDirection { get; }
        public Queue<int> DestinationQueue { get; set; }
        public event EventHandler Idle;
        public event EventHandler<int> FloorButtonPressed;
        public event EventHandler<PassingFloorEventArgs> PassingFloor;
        public event EventHandler<int> StoppedAtFloor;

        #region Test Helpers

        public void TestInvokeIdleEvent()
        {
            Idle?.Invoke(this, new EventArgs());
        }

        public void TestInvokeFloorButtonPressedEvent(int floorNum)
        {
            FloorButtonPressed?.Invoke(this, floorNum);
        }

        public void TestInvokePassingFloorEvent(ElevatorDirection direction, int floor)
        {
            PassingFloor?.Invoke(this, new PassingFloorEventArgs {Direction = direction, PassingFloorNumber = floor});
        }

        public void TestInvokeStoppedAtFloorEvent(int floor)
        {
            StoppedAtFloor?.Invoke(this, floor);
        }

        private readonly IList<string> _calledMethods;

        public IReadOnlyList<string> CalledMethods => _calledMethods as IReadOnlyList<string>;

        #endregion
    }
}
