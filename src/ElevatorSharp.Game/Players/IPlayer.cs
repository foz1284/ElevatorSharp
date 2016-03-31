namespace ElevatorSharp.Game.Players
{
    public interface IPlayer
    {
        /// <summary>
        /// Do stuff with the elevators and floors. Handle events on these objects.
        /// </summary>
        /// <param name="elevators"></param>
        /// <param name="floors"></param>
        void Init(IElevator[] elevators, IFloor[] floors);
    }
}