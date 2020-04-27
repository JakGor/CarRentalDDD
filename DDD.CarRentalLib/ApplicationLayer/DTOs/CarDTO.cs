using DDD.Base.DomainModelLayer.Models;
using DDD.CarRentalLib.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.CarRentalLib.ApplicationLayer.DTOs
{
    public enum CarStatusDTO
    {
        Free = 0,
        Reserved = 1,
        Rented = 2
    }
    public class CarDTO
    {
        public Guid CarId { get; set; }
        public string RegistrationNumber { get; set; }
        public double CurrentDistance { get;  set; }
        public double TotalDistance { get;  set; }
        public CarStatusDTO Status { get; set; }
        public PositionDTO CurrentPosition { get; set; }
        
    }
}
