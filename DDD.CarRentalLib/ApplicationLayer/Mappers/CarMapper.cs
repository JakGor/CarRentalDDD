using DDD.CarRentalLib.ApplicationLayer.DTOs;
using DDD.CarRentalLib.DomainModelLayer.Models;
using DDD.Base.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace DDD.CarRentalLib.ApplicationLayer.Mappers
{
    public class CarMapper
    {
        public List<CarDTO> Map(IEnumerable<Car> cars)
        {
            return cars.Select(c => Map(c)).ToList();
        }
        public CarDTO Map(Car c)
        {
            return new CarDTO()
            {
                CarId = c.Id,
                RegistrationNumber = c.RegistrationNumber,
                CurrentDistance = c.CurrentDistance.Value,
                TotalDistance = c.TotalDistance.Value,
                CurrentPosition = Map(c.CurrentPosition),
                Status = (CarStatusDTO)c.Status
                


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
