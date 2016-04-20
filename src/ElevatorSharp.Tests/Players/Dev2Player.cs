using ElevatorSharp.Game.Players;
using System.Linq;
using ElevatorSharp.Game;


namespace ElevatorSharp.Tests.Players
{
    public class Dev2Player : IPlayer
    {
        public void Init(IElevator[] elevators, IFloor[] floors)
        {
            foreach (IFloor floor in floors)
            {
                foreach (IElevator elevator in elevators.OrderBy(e => e.LoadFactor))
                {
                    elevator.FloorButtonPressed += Elevator_FloorButtonPressed;

                    floor.DownButtonPressed += Floor_DownButtonPressed;
                    floor.UpButtonPressed += Floor_DownButtonPressed;
                }
            }
        }

        private void Floor_DownButtonPressed(object sender, IElevator[] e)
        {
            foreach (IElevator elevator in e.OrderBy(el => el.LoadFactor))
            {
                if (!elevator.DestinationQueue.Contains((sender as IFloor).FloorNum))
                {
                    elevator.GoToFloor((sender as IFloor).FloorNum);
                }
            }
        }

        private void Elevator_FloorButtonPressed(object sender, int e)
        {
            if (!(sender as IElevator).DestinationQueue.Contains(e))
            {
                (sender as IElevator).GoToFloor(e);
            }
        }
    }
}
