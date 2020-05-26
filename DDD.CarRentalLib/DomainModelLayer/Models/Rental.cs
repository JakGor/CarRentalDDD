using System;
using System.Collections.Generic;
using System.Text;
using DDD.Base.DomainModelLayer.Models;
using DDD.Base.DomainModelLayer.Events;
using DDD.CarRentalLib.DomainModelLayer.Policies;
using DDD.CarRentalLib.DomainModelLayer.Interfaces;

namespace DDD.CarRentalLib.DomainModelLayer.Models
{
    public class Rental : AggregateRoot
    {
        public Guid RentalId { get; set; }
        public DateTime Started { get; protected set; }
        public DateTime Finished { get; protected set; }
        public Money Total { get; protected set; }
        public double FreeMinutes { get; protected set; }
        public Guid DriverId { get; protected set; }
        public Guid CarId {get; protected set;}
        public Guid PassengerId { get; protected set; }
        private IFreeMinutesPolicy _policy; 

        public Rental(Guid rentalId, Guid carId, Guid driverId, DateTime started, IDomainEventPublisher eventPublisher)
            : base(rentalId, eventPublisher)
        {
            this.Id = rentalId;
            this.CarId = carId;
            this.DriverId = driverId;
            this.Started = started;
            this.Total = Money.Zero;
            this.FreeMinutes = 0;
        }
        public void RegisterPolicy(IFreeMinutesPolicy policy)
        {
            this._policy = policy ?? throw new Exception("Empty free minutes policy");
        }
        public void ReturnCar(DateTime finished)
        {
            if(finished < Started)
                throw new Exception($"Return date and time is earlier than start date and time.");
            this.Finished = finished;
            //time of rental in minutes
            var timeInMinutes = Math.Round((this.Finished - this.Started).TotalMinutes,2);
            //calculate cost
            double priceForMinute = 0.75;
            decimal totalCost = (decimal)(timeInMinutes * priceForMinute);           
            Total = new Money(totalCost);
            //apply free minutes policy
            if(this._policy != null)
            {
                FreeMinutes = this._policy.CalculateFreeMinutes(timeInMinutes);
            }
        }
        public void ReservePassenger(Guid passengerId)
        {
            this.PassengerId = passengerId;
        }
        public void PassengerAtTarget()
        {
            this.PassengerId = Guid.Empty;
            
        }
    }
}
