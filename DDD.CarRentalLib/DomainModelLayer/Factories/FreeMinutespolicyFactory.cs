using System;
using System.Collections.Generic;
using System.Text;
using DDD.CarRentalLib.DomainModelLayer.Interfaces;
using DDD.CarRentalLib.DomainModelLayer.Models;
using DDD.CarRentalLib.DomainModelLayer.Policies;

namespace DDD.CarRentalLib.DomainModelLayer.Factories
{
    public class FreeMinutespolicyFactory
    {
        public IFreeMinutesPolicy Create(Rental rental)
        {
            IFreeMinutesPolicy policy = new StandardFreeMinutesPolicy();
            //If customer make a rental at early morning, PremiumFreeMiutes policy will be applied
            if (rental.Started.Hour < 7)
                policy = new PremiumFreeMinutesPolicy();
            return policy;
        }
    }
}
