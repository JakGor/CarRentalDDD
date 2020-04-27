using System;
using System.Collections.Generic;
using System.Text;
using DDD.CarRentalLib.ApplicationLayer.DTOs;
using DDD.CarRentalLib.DomainModelLayer.Models;
using DDD.Base.DomainModelLayer.Models;
using System.Linq;

namespace DDD.CarRentalLib.ApplicationLayer.Mappers
{
    public class RentalMapper
    {
        public List<RentalDTO> Map(IEnumerable<Rental> rentals)
        {
            return rentals.Select(r => Map(r)).ToList();
        }
        public RentalDTO Map(Rental rental)
        {
            return new RentalDTO()
            {
                RentalId = rental.RentalId,
                Started = rental.Started,
                Finished = rental.Finished,
                Total = rental.Total.Amount,
                FreeMinutes = rental.FreeMinutes,
                CarId = rental.CarId,
                DriverId = rental.DriverId
            };
        }
    }
}
