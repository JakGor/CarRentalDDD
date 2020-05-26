using System;
using System.Collections.Generic;
using System.Text;
using DDD.CarRentalLib.DomainModelLayer.Interfaces;
using DDD.Base.DomainModelLayer.Models;
namespace DDD.CarRentalLib.DomainModelLayer.Policies
{
    public class StandardFreeMinutesPolicy : IFreeMinutesPolicy
    {
        public string Name { get; protected set; }
        public StandardFreeMinutesPolicy()
        {
            this.Name = "Standard free minutes policy";
        }
        public double CalculateFreeMinutes(double totalTime)
        {
            return totalTime * 0.05;
        }
    }
}
