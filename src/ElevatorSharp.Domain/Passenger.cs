namespace ElevatorSharp.Domain
{
    public class Passenger
    {
        public int CurrentFloor { get; set; }
        public int DestinationFloor { get; set; }
        public bool Arrived { get; set; }

        public void PressFloorButton(Floor floor)
        {
            if (DestinationFloor < CurrentFloor)
            {
                floor.PressDownButton();
            }
            else
            {
                floor.PressUpButton();
            }
        }
    }
}