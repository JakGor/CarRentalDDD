using System;
using System.Collections.Generic;
using System.Text;
using DDD.Base.DomainModelLayer.Models;
using DDD.Base.DomainModelLayer.Events;
using DDD;
namespace DDD.CarRentalLib.DomainModelLayer.Models
{
    public class Driver: AggregateRoot
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string LicenceNumber { get; protected set; }
        public double FreeMinutes { get; protected set; }
        public Money Balance { get; protected set; }

        public Driver(Guid driverId, string firstName, string lastName, string licenceNumber, double freeMinutes, IDomainEventPublisher eventPublisher)
            : base(driverId, eventPublisher)
        {
            if (String.IsNullOrEmpty(licenceNumber)) throw new Exception("Licence number is null or empty");
            if (String.IsNullOrEmpty(firstName)) throw new Exception("First name is null or empty");
            if (String.IsNullOrEmpty(lastName)) throw new Exception("Last name is null or empty");
            FirstName = firstName;
            LastName = lastName;
            LicenceNumber = licenceNumber;
            FreeMinutes = freeMinutes;
            Balance = Money.Zero;
        }
        public void AddFreeMinutes(double freeMinutes)
        {
            FreeMinutes += freeMinutes;
        }
        public void ChangeBalance(bool increase, Money amount)
        {
            if (increase == true)
                Balance += amount;
            else
                Balance -= amount;
                
        }
        

    }
}
