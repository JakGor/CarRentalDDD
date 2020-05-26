using System;
using System.Collections.Generic;
using System.Text;
using DDD.Base.DomainModelLayer.Interfaces;
using DDD.Base.DomainModelLayer.Models;
using DDD.CarRentalLib.DomainModelLayer.Interfaces;
using DDD.CarRentalLib.DomainModelLayer.Models;

namespace DDD.CarRentalLib.InfrastructureLayer
{
    public class MemoryCarRentalUnitOfWork : ICarRentalUnitOfWork
    {
        public IRepository<Driver> DriverRepository { get; protected set; }
        public IRepository<Car> CarRepository { get; protected set; }
        public IRepository<Rental> RentalRepository { get; protected set; }
        public IRepository<Passenger> PassengerRepository { get; protected set; }
        public MemoryCarRentalUnitOfWork(
            IRepository<Car> carRepository,
            IRepository<Driver> driverRepository,
            IRepository<Rental> rentalRepository,
            IRepository<Passenger> passengerRepository)
        {
            CarRepository = carRepository;
            DriverRepository = driverRepository;
            RentalRepository = rentalRepository;
            PassengerRepository = passengerRepository;
        }
        public MemoryCarRentalUnitOfWork()
        {
            CarRepository = new MemoryRepository<Car>();
            DriverRepository = new MemoryRepository<Driver>();
            RentalRepository = new MemoryRepository<Rental>();
            PassengerRepository = new MemoryRepository<Passenger>();
        }
        public void Commit()
        {}

        public void Dispose()
        {}

        public void RejectChanges()
        {}
    }
}
