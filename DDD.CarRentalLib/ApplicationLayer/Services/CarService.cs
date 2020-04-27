using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using DDD.CarRentalLib.ApplicationLayer.DTOs;
using DDD.CarRentalLib.ApplicationLayer.Interfaces;
using DDD.CarRentalLib.DomainModelLayer.Interfaces;
using DDD.CarRentalLib.DomainModelLayer.Models;
using DDD.CarRentalLib.ApplicationLayer.Services;
using DDD.CarRentalLib.ApplicationLayer.Mappers;
using DDD.Base.DomainModelLayer.Events;

namespace DDD.CarRentalLib.ApplicationLayer.Services
{
    public class CarService : ICarService
    {
        private ICarRentalUnitOfWork _unitOfWork;
        private CarMapper _carMapper;
        private IDomainEventPublisher _domainEventPublisher;

        public  CarService(ICarRentalUnitOfWork unitOfWork,
            CarMapper carMapper,
            IDomainEventPublisher domainEventPublisher)
        {
            this._unitOfWork = unitOfWork;
            this._carMapper = carMapper;
            this._domainEventPublisher = domainEventPublisher;
        }

        public void CreateCar(CarDTO carDTO)
        {
            Expression<Func<Car, bool>> expressionPredicate = x => x.RegistrationNumber == carDTO.RegistrationNumber;
            Car car = this._unitOfWork.CarRepository.Find(expressionPredicate).FirstOrDefault();
            if (car != null)
                throw new Exception($"Car with'{carDTO.RegistrationNumber}' registration number already exists");
            car = new Car(carDTO.CarId, carDTO.RegistrationNumber, carDTO.CurrentDistance, carDTO.TotalDistance, carDTO.CurrentPosition.X,
                carDTO.CurrentPosition.Y, this._domainEventPublisher);
            this._unitOfWork.CarRepository.Insert(car);
            this._unitOfWork.Commit();
        }

        public List<CarDTO> GetAllCars()
        {
            IList<Car> cars = this._unitOfWork.CarRepository.GetAll()
                ?? throw new Exception("Could not find cars");
            List<CarDTO> result = this._carMapper.Map(cars);
            return result;
        }
    }
}
