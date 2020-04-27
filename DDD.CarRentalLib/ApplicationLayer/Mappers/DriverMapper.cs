using System;
using System.Collections.Generic;
using System.Text;
using DDD.CarRentalLib.ApplicationLayer.DTOs;
using DDD.CarRentalLib.DomainModelLayer.Models;
using DDD.Base.DomainModelLayer.Models;
using System.Linq;

namespace DDD.CarRentalLib.ApplicationLayer.Mappers
{
    public class DriverMapper
    {
        public List<DriverDTO> Map(IEnumerable<Driver> drivers)
        {
            return drivers.Select(d => Map(d)).ToList();
        }
        public DriverDTO Map(Driver driver)
        {
            return new DriverDTO()
            {
                DriverId = driver.Id,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                LicenceNumber = driver.LicenceNumber,
                FreeMinutes = driver.FreeMinutes
            };
        }
    }
}
