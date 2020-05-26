using System;
using System.Collections.Generic;
using System.Text;
using DDD.CarRentalLib.ApplicationLayer.DTOs;
using DDD.CarRentalLib.DomainModelLayer.Models;
using DDD.Base.DomainModelLayer.Models;
using System.Linq;

namespace DDD.CarRentalLib.ApplicationLayer.Mappers
{
    public class PassengerMapper
    {
        public List<PassengerDTO> Map(IEnumerable<Passenger> passengers)
        {
            return passengers.Select(p => Map(p)).ToList();
        }
        public PassengerDTO Map(Passenger p)
        {
            return new PassengerDTO()
            {
                PassengerId = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Status = (PassengerStatusDTO)p.Status,
                CurrentPosition = Map(p.CurrentPosition),
                TargetAddressStreet = p.TargetAddress.Street,
                TargetAddressNumber = p.TargetAddress.Number,
                MoneyToOffer = p.MoneyToOffer.Amount

            };
        }
        public PositionDTO Map(Position p)
        {
            return new PositionDTO()
            {
                X = p.X,
                Y = p.Y,
                Unit = p.Unit
            };
        }
    }
}
