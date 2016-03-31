using System.Collections.Generic;
using System.Linq;
using ElevatorSharp.Domain.DataTransferObjects;
using ElevatorSharp.Game;
using ElevatorSharp.Game.Players;

namespace ElevatorSharp.Domain
{
    internal class Skyscraper
    {
        #region Fields
        #endregion

        #region Properties

        internal IList<Elevator> Elevators { get; }
        internal IList<Floor> Floors { get; }
        #endregion

        #region Constructors

        internal Skyscraper(SkyscraperDto skyscraperDto)
        {
            Elevators = new List<Elevator>();
            foreach (var elevatorDto in skyscraperDto.Elevators)
            {
                var elevator = new Elevator(elevatorDto.ElevatorIndex, elevatorDto.MaxPassengerCount);
                Elevators.Add(elevator);
            }

            Floors = new List<Floor>();
            for (var i = 0; i < skyscraperDto.Floors.Count; i++)
            {
                Floors.Add(new Floor(i));
            }
        }
        #endregion

        #region Methods

        internal void LoadPlayer(IPlayer player)
        {
            player.Init(Elevators.ToArray(), Floors.ToArray());
        }
        #endregion
    }
}