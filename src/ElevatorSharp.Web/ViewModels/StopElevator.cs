namespace ElevatorSharp.Web.ViewModels
{
    public class StopElevator
    {
        public StopElevator(int index)
        {
            ElevatorIndex = index;
        }

        public int ElevatorIndex { get; set; }
    }
}