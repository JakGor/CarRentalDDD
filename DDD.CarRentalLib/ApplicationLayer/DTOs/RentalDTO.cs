using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.CarRentalLib.ApplicationLayer.DTOs
{
    public class RentalDTO
    {
        public Guid RentalId { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public decimal Total { get; set; }
        public double FreeMinutes { get; set; }
        public Guid DriverId { get; set; }

        public Guid CarId { get; set; }
    }
}
