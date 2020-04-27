using System;
using System.Collections.Generic;
using System.Text;
using DDD.Base.DomainModelLayer.Events;
using DDD.CarRentalLib.DomainModelLayer.Models;

namespace DDD.CarRentalLib.DomainModelLayer.Factories
{
    public class RentalFactory
    {
        private IDomainEventPublisher _domainEventPublisher;

        public RentalFactory(IDomainEventPublisher domainEventPublisher)
        {
            this._domainEventPublisher = domainEventPublisher;
        }

        public Rental Create(Guid rentalId, DateTime started, Driver driver, Car car)
        {
            CheckIfCarIsFree(car);
            return new Rental(rentalId, car.Id, driver.Id, started, this._domainEventPublisher);
        }
        private void CheckIfCarIsFree(Car car)
        {
            if (car.Status != CarStatus.Free) throw new Exception($"Car '{car.Id}' is not free");
        }
    }
}
