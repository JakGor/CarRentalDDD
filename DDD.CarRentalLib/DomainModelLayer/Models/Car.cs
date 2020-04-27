using System;
using System.Collections.Generic;
using System.Text;
using DDD.Base.DomainModelLayer.Events;
using DDD.Base.DomainModelLayer.Models;


namespace DDD.CarRentalLib.DomainModelLayer.Models
{
    public enum CarStatus
    {
        Free = 0,
        Reserved = 1,
        Rented = 2
    }
    public class Car: AggregateRoot
    {
        public Car(Guid carId, string registrationNumber, double currentDistance, double totalDistance, double xPosition, double yPosition,  IDomainEventPublisher domainEventPublisher) 
            : base(carId, domainEventPublisher)
        {
            Id = carId;
            RegistrationNumber = registrationNumber;
            CurrentDistance = new Distance(currentDistance);
            TotalDistance = new Distance(totalDistance);
            Status = CarStatus.Free;
            CurrentPosition = new Position(xPosition, yPosition);           
        }

        public string RegistrationNumber { get; protected set; }
        public Distance CurrentDistance { get; protected set; }
        public Distance TotalDistance { get; protected set; }
        public CarStatus Status { get; protected set; }
        public Position CurrentPosition { get; protected set; }

        public void MakeCarRented()
        {
            this.Status = CarStatus.Rented;
        }
        public void MakeCarFree()
        {
            this.Status = CarStatus.Free;
        }
        public void ChangePosition(Position newPosition)
        {
            this.CurrentPosition = newPosition;
        }

    }
}
