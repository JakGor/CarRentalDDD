using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using DDD.CarRentalLib.ApplicationLayer.DTOs;
using DDD.CarRentalLib.ApplicationLayer.Interfaces;
using DDD.CarRentalLib.DomainModelLayer.Interfaces;
using DDD.CarRentalLib.DomainModelLayer.Models;
using DDD.CarRentalLib.DomainModelLayer.Policies;
using DDD.CarRentalLib.DomainModelLayer.Factories;
using DDD.CarRentalLib.ApplicationLayer.Services;
using DDD.CarRentalLib.ApplicationLayer.Mappers;
using DDD.Base.DomainModelLayer.Events;
using DDD.CarRentalLib.InfrastructureLayer;

namespace DDD.CarRentalLib.ApplicationLayer.Services
{
    public class RentalService : IRentalService
    {
        private ICarRentalUnitOfWork _unitOfWork;
        private RentalFactory _rentalFactory;
        private RentalMapper _rentalMapper;
        private FreeMinutespolicyFactory _freeMinutespolicyFactory;
        private PositionService _positionService;
        private IDomainEventPublisher _domainEventPublisher;
        

        public RentalService(ICarRentalUnitOfWork unitOfWork,
            RentalFactory rentalFactory,
            RentalMapper rentalMapper,
            FreeMinutespolicyFactory freeMinutespolicyFactory,
            PositionService positionService,
            IDomainEventPublisher domainEventPublisher)
        {
            this._unitOfWork = unitOfWork;
            this._rentalFactory = rentalFactory;
            this._rentalMapper = rentalMapper;
            this._freeMinutespolicyFactory = freeMinutespolicyFactory;
            this._positionService = positionService;
            this._domainEventPublisher = domainEventPublisher;
            
        }
     
        public void MakeRental(Guid rentalId, Guid carId, Guid driverId, DateTime started)
        {
            Car car = this._unitOfWork.CarRepository.Get(carId)
                ?? throw new Exception($"Could not find the car: '{carId}'");
            Driver driver = this._unitOfWork.DriverRepository.Get(driverId)
                ?? throw new Exception($"Could not find the driver: '{driverId}'");

            Rental rental = this._rentalFactory.Create(rentalId, started, driver, car);

            IFreeMinutesPolicy policy = this._freeMinutespolicyFactory.Create(rental);
            //Register free minutes policy in rental object
            rental.RegisterPolicy(policy);
            // set car status to "rented"
            car.MakeCarRented();

            this._unitOfWork.RentalRepository.Insert(rental);
            this._unitOfWork.Commit();
        }

        public void ReturnCar(Guid rentalId, Guid carId, Guid driverId, DateTime finished)
        {
            Car car = this._unitOfWork.CarRepository.Get(carId)
                ?? throw new Exception($"Could not find the car: '{carId}'");
            Driver driver = this._unitOfWork.DriverRepository.Get(driverId)
                ?? throw new Exception($"Could not find the driver: '{driverId}'");
            Rental rental = this._unitOfWork.RentalRepository.Get(rentalId)
                ?? throw new Exception($"Could not find rental '{rentalId}'");

            //change position of car
            this._positionService.CarPosition(car);
            //end rental (set finished time value, calculate cost and amount of free minutes)
            rental.ReturnCar(finished);
            //set car status to "free"
            car.MakeCarFree();
            //add calculated amount of free miutes to driver's account
            driver.AddFreeMinutes(rental.FreeMinutes);

            this._unitOfWork.Commit();
        }
        public List<RentalDTO> GetAllRentals()
        {
            IList<Rental> rentals = this._unitOfWork.RentalRepository.GetAll();

            List<RentalDTO> result = this._rentalMapper.Map(rentals);

            return result;
        }
    }
}
