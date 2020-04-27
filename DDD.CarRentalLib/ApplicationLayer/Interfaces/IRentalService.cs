using System;
using System.Collections.Generic;
using System.Text;
using DDD.Base.ApplicationLayer.Services;
using DDD.CarRentalLib.ApplicationLayer.DTOs;

namespace DDD.CarRentalLib.ApplicationLayer.Interfaces
{
    public interface IRentalService: IApplicationService
    {
        void MakeRental(Guid rentalId, Guid carId, Guid driverId, DateTime started);

        void ReturnCar(Guid rentalId, Guid carId, Guid driverId, DateTime finished);
        List<RentalDTO> GetAllRentals();
    }
}
