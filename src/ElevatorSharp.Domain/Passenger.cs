namespace ElevatorSharp.Domain
{
    internal class Passenger
    {
        internal int CurrentFloor { get; set; }
        internal int DestinationFloor { get; set; }
        internal bool Arrived { get; set; }
    }
}