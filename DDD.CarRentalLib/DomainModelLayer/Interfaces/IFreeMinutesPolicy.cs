using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.CarRentalLib.DomainModelLayer.Interfaces
{
    public interface IFreeMinutesPolicy
    {
        string Name { get; }
        double CalculateFreeMinutes(double totalTime);
    }
}
