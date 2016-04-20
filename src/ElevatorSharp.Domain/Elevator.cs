using System;
using System.Collections.Generic;
using ElevatorSharp.Game;
using System.Linq;

namespace ElevatorSharp.Domain
{
    internal sealed class Elevator : IElevator
    {
        private Queue<int> destinationQueue;

        #region Events
        public event EventHandler Idle;
        public event EventHandler<int> FloorButtonPressed;
        public event EventHandler<PassingFloorEventArgs> PassingFloor;
        public event EventHandler<int> StoppedAtFloor; 
        #endregion

        #region Properties
        public ElevatorDirection DestinationDirection { get; set; }

        public Queue<int> DestinationQueue
        {
            get { return destinationQueue; }
            set
            {
                IsDestinationQueueModified = true;
                destinationQueue = value;
            }
        }

        public int Index { get; }
        public int CurrentFloor { get; set; }
        public bool GoingUpIndicator { get; set; }
        public bool GoingDownIndicator { get; set; }
        public int MaxPassengerCount { get; }
        public decimal LoadFactor { get; set; }
        public int[] PressedFloors { get; set; }

        public List<Passenger> Riders = new List<Passenger>();
        public decimal MaxSpeed = 20;
        public decimal Velocity = 0;
        public decimal Accelaration = (decimal)1.2192;
        public decimal Decelaration { get; set; }
        public int CurrentDestination { get; set; }
        public decimal FloorHeight = 3;
        public decimal HeightAboveCurrentFloor { get; set; }
        #endregion

        #region Constructors
        internal Elevator(int index)
        {
            Index = index;
            DestinationQueue = new Queue<int>();
            NewDestinations = new Queue<int>();
            JumpQueueDestinations = new Queue<int>();
        }

        internal Elevator(int index, int maxPassengerCount) : this(index)
        {
            MaxPassengerCount = maxPassengerCount;
        }
        #endregion

        #region Updating
        public void Update(Skyscraper skyscraper)
        {
            if (destinationQueue.Count > 0)
            {
                if (destinationQueue.Peek() > CurrentFloor)
                {
                    if (Velocity < 20)
                    {
                        Velocity = Velocity + (Accelaration / 1000);
                    }
                    HeightAboveCurrentFloor = HeightAboveCurrentFloor + (decimal)Velocity / 1000;
                    if (HeightAboveCurrentFloor > 3)
                    {
                        HeightAboveCurrentFloor = 0;
                        CurrentFloor++;
                    }
                    //TODO: add accelaration/Deceleration
                }
                else if (destinationQueue.Peek() < CurrentFloor || HeightAboveCurrentFloor > 0)
                {
                    if (Velocity < 20)
                    {
                        Velocity = Velocity + (Accelaration / 1000);
                    }
                    HeightAboveCurrentFloor = HeightAboveCurrentFloor - (decimal)Velocity/1000;
                    if (HeightAboveCurrentFloor < 0)
                    {
                        HeightAboveCurrentFloor = (decimal)2.9999999;
                        CurrentFloor--;
                    }
                }
                else if(HeightAboveCurrentFloor == 0)
                {
                    //Arrived at destination
                    Velocity = 0;
                    destinationQueue.Dequeue();
                    UnloadRiders();//TODO: Need to stop elevator for an amount of time whilst people exit
                    LoadRiders(skyscraper);//TODO: Need to stop elevator for an amount of time whilst people Enter
                }
            }
            else
            {
                LoadRiders(skyscraper);
            }
        }


        private void LoadRiders(Skyscraper skyscraper)
        {
            Floor currentFloor = skyscraper.Floors.Single(f => f.FloorNum == CurrentFloor);
            foreach (Passenger rider in currentFloor.RidersWaiting)
            {
                if (Riders.Count == MaxPassengerCount)
                { 
                    break;
                }
                Riders.Add(rider);
                rider.InElevator = true;
                
                OnFloorButtonPressed(rider.DestinationFloor);
            }
            currentFloor.DownButtonActive = false;
            currentFloor.UpButtonActive = false;
            currentFloor.RidersWaiting.RemoveAll(r => r.InElevator);

            foreach (Passenger rider in currentFloor.RidersWaiting)
            {
                currentFloor.RiderPressesButton(skyscraper, rider);
            }
        }

        public int UnloadedCount = 0;
        private void UnloadRiders()
        {
            foreach (Passenger r in Riders.Where(r=> r.DestinationFloor == CurrentFloor))
            {
                UnloadedCount++;
            }

            Riders.RemoveAll(r => r.DestinationFloor == CurrentFloor);
            //TODO: record that a rider has reached their destination - probably an event that a game monitor will be watching or could unload to the floor and let that deal with it.
        }
        #endregion

        #region Event Invocators
        public void OnIdle()
        {
            Idle?.Invoke(this, EventArgs.Empty);
        }

        public void OnFloorButtonPressed(int floorNumber)
        {
            FloorButtonPressed?.Invoke(this, floorNumber);
        }

        public void OnPassingFloor(PassingFloorEventArgs e)
        {
            PassingFloor?.Invoke(this, e);
        }

        public void OnStoppedAtFloor(int e)
        {
            StoppedAtFloor?.Invoke(this, e);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Queue the elevator to go to specified floor number. If you specify true as second argument, the elevator will go to that floor directly, and then go to any other queued floors.
        /// </summary>
        /// <param name="floor"></param>
        public void GoToFloor(int floor)
        {
            GoToFloor(floor, false);
        }

        /// <summary>
        /// Queue the elevator to go to specified floor number. 
        /// If you specify true as second argument, the elevator will go to that floor directly, and then go to any other queued floors.
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="jumpQueue"></param>
        public void GoToFloor(int floor, bool jumpQueue)
        {
            if (!jumpQueue)
            {
                DestinationQueue.Enqueue(floor);
                NewDestinations.Enqueue(floor);
            }
            else
            {
                var items = DestinationQueue.ToArray();
                DestinationQueue.Clear();
                DestinationQueue.Enqueue(floor);
                foreach (var item in items)
                    DestinationQueue.Enqueue(item);

                JumpQueueDestinations.Enqueue(floor);
            }
        }

        /// <summary>
        /// Checks the destination queue for any new destinations to go to. 
        /// Note that you only need to call this if you modify the destination queue explicitly.
        /// </summary>
        public void CheckDestinationQueue()
        {
            if (DestinationQueue.Count > 0)
            {
                var nextFloor = DestinationQueue.Dequeue();
                GoToFloor(nextFloor);
            }
            else
            {
                OnIdle();
            }
        }

        /// <summary>
        /// Clear the destination queue and stop the elevator if it is moving. 
        /// Note that you normally don't need to stop elevators - it is intended for advanced solutions with in-transit rescheduling logic. 
        /// Also, note that the elevator will probably not stop at a floor, so passengers will not get out.
        /// </summary>
        public void Stop()
        {
            DestinationQueue.Clear();
            StopElevator = true;
            IsDestinationQueueModified = true;
        }
        #endregion

        #region Client-side tracking
        public Queue<int> NewDestinations { get; set; }
        public Queue<int> JumpQueueDestinations { get; set; }
        public bool StopElevator { get; set; }
        public bool IsDestinationQueueModified { get; set; }
        #endregion
    }
}