namespace ElevatorSharp.Domain
{
    internal class Passenger
    {
        internal Passenger(int destinationfloor)
        {
            DestinationFloor = destinationfloor;
        }

        internal int CurrentFloor { get; set; }
        internal int DestinationFloor { get; set; }
        internal bool Arrived { get; set; }
        internal bool InElevator { get; set; }
    }
}