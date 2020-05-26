using System;
using System.Collections.Generic;
using System.Text;
using DDD.Base.ApplicationLayer.Services;
using DDD.CarRentalLib.ApplicationLayer.DTOs;

namespace DDD.CarRentalLib.ApplicationLayer.Interfaces
{
    public interface IPassengerService: IApplicationService
    {
        void CreatePassenger(PassengerDTO passengerDTO);
        void SetTarget(Guid passengerId, string targetAddressStreet, string targetAddressNumber, decimal moneyAmount);
        List<PassengerDTO> GetAllPassengers();
        
    }
}
