using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.CarRentalLib.ApplicationLayer.DTOs
{
    public enum PassengerStatusDTO
    {
        Free = 0,
        Waiting = 1,
        Taken = 2,
        TargetNotSelected = 3
    }
    public class PassengerDTO
    {
        public Guid PassengerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PassengerStatusDTO Status { get; set; }
        public PositionDTO CurrentPosition { get; set; }
        public string TargetAddressStreet { get; set; }
        public string TargetAddressNumber { get; set; }
        public decimal MoneyToOffer { get; set; }

    }
}
