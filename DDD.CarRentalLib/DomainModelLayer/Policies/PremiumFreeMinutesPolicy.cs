using System;
using System.Collections.Generic;
using System.Text;
using DDD.CarRentalLib.DomainModelLayer.Interfaces;
using DDD.Base.DomainModelLayer.Models;

namespace DDD.CarRentalLib.DomainModelLayer.Policies
{
    public class PremiumFreeMinutesPolicy : IFreeMinutesPolicy
    {
        public string Name { get; protected set; }
        public PremiumFreeMinutesPolicy()
        {
            this.Name = "Premium free minutes policy";
        }
        //If customer return a car in less than 1 hour, he will receive even more free minutes
        public double CalculateFreeMinutes(double totalTime)
        {
            if (totalTime < 60)
                return totalTime * 0.15;
            else
                return totalTime * 0.1;
        }
    }
}
