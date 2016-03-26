using System.Collections.Generic;
using System.Linq;
using ElevatorSharp.Domain.DataTransferObjects;
using ElevatorSharp.Domain.Players;

namespace ElevatorSharp.Domain
{
    public class Skyscraper
    {
        #region Fields
        #endregion

        #region Properties
        public IList<IElevator> Elevators { get; }
        public IList<IFloor> Floors { get; }
        #endregion

        #region Constructors
        public Skyscraper(SkyscraperDto skyscraperDto)
        {
            Elevators = new List<IElevator>();
            foreach (var elevatorDto in skyscraperDto.Elevators)
            {
                var elevator = new Elevator(elevatorDto.ElevatorIndex, elevatorDto.MaxPassengerCount);
                Elevators.Add(elevator);
            }

            Floors = new List<IFloor>();
            for (var i = 0; i < skyscraperDto.Floors.Length; i++)
            {
                Floors.Add(new Floor(i));
            }
        }
        #endregion

        #region Methods
        public void LoadPlayer(IPlayer player)
        {
            player.Init(Elevators, Floors);
        }
        #endregion
    }
}