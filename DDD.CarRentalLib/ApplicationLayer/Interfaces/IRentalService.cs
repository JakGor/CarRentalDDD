using System;
using System.Collections.Generic;
using System.Text;
using DDD.Base.ApplicationLayer.Services;
using DDD.CarRentalLib.ApplicationLayer.DTOs;
using DDD.CarRentalLib.DomainModelLayer.Models;

namespace DDD.CarRentalLib.ApplicationLayer.Interfaces
{
    public interface IRentalService: IApplicationService
    {
        void MakeRental(Guid rentalId, Guid carId, Guid driverId, DateTime started);

        void ReturnCar(Guid rentalId, Guid carId, Guid driverId, DateTime finished);
        public void ReservePassenger(Guid rentalId, Guid passengerId);
        public void TakePassenger(Guid rentalId, Guid passengerId);
        public void PassengerAtTarget(Guid rentalId);
        List<RentalDTO> GetAllRentals();
        public Dictionary<PassengerDTO, Distance> ShowPassengersWithDistance(Guid carId);
    }
}
