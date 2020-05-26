using System;
using System.Collections.Generic;
using System.Text;
using DDD.CarRentalLib.DomainModelLayer.Models;
using DDD.Base.DomainModelLayer.Models;

namespace DDD.CarRentalLib.DomainModelLayer.Models
{
    public class Address : ValueObject
    {
        public string Street { get; protected set; }
        public string Number { get; protected set; }
        public Address(string street, string number)
        {
            Street = street;
            Number = number;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street + " " + Number;
        }
        public override string ToString()
        {
            return Street + " " + Number;
        }
    }
}
