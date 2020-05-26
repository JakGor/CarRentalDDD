using System;
using System.Collections.Generic;
using System.Text;
using DDD.Base.DomainModelLayer.Models;
using DDD.Base.DomainModelLayer.Events;

namespace DDD.CarRentalLib.DomainModelLayer.Models
{
    public enum PassengerStatus
    {
        Free = 0,
        Waiting = 1,
        Taken = 2,
        TargetNotSelected = 3
    }
    public class Passenger: AggregateRoot
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public PassengerStatus Status { get; protected set; }
        public Position CurrentPosition { get; protected set; }
        public Address TargetAddress { get; protected set; }
        public Money MoneyToOffer { get; protected set; }
        public Passenger(Guid passengerId, string firstName, string lastName, double XPosition, double YPosition, IDomainEventPublisher domainEventPublisher)
            :base(passengerId, domainEventPublisher)
        {
            Id = passengerId;
            FirstName = firstName;
            LastName = lastName;
            CurrentPosition = new Position(XPosition, YPosition);
            Status = PassengerStatus.TargetNotSelected;
            TargetAddress = new Address("", "");
            MoneyToOffer = Money.Zero;
        }
        public void SelectTarget(string targetStreet, string targetNumber, decimal moneyAmount)
        {
            this.Status = PassengerStatus.Free;
            this.TargetAddress = new Address(targetStreet, targetNumber);
            this.MoneyToOffer = new Money(moneyAmount);
        }
        public void PassengerReserved()
        {
            this.Status = PassengerStatus.Waiting;
        }
        public void PassengerTaken()
        {
            this.Status = PassengerStatus.Taken;
        }
        public void PassengerAtTarget()
        {
            this.TargetAddress = new Address("", "");
            this.MoneyToOffer = Money.Zero;
            this.Status = PassengerStatus.TargetNotSelected;
        }
        public void ChangePosition(Position newPosition)
        {
            this.CurrentPosition = newPosition;
        }
    }
}
