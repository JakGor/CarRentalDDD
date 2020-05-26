using System;
using System.Collections.Generic;
using System.Text;
using DDD.CarRentalLib.DomainModelLayer.Models;
using DDD.Base.DomainModelLayer.Interfaces;

namespace DDD.CarRentalLib.DomainModelLayer.Interfaces
{
    public interface ICarRentalUnitOfWork: IUnitOfWork, IDisposable
    {
        IRepository<Driver> DriverRepository { get; }
        IRepository<Car> CarRepository { get; }
        IRepository<Rental> RentalRepository { get; }
        IRepository<Passenger> PassengerRepository { get; }
        
    }
}
