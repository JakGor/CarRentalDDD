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
    public class DriverService : IDriverService
    {
        private ICarRentalUnitOfWork _unitOfWork;
        private DriverMapper _driverMapper;
        private IDomainEventPublisher _domainEventPublisher;

        public DriverService(ICarRentalUnitOfWork unitOfWork,
            DriverMapper driverMapper,
            IDomainEventPublisher domainEventPublisher)
        {
            this._unitOfWork = unitOfWork;
            this._driverMapper = driverMapper; 
            this._domainEventPublisher = domainEventPublisher;
        }
        public void CreateDriver(DriverDTO driverDTO)
        {
            Expression<Func<Driver, bool>> expressionPredicate = x => x.LicenceNumber == driverDTO.LicenceNumber;
            Driver driver = this._unitOfWork.DriverRepository.Find(expressionPredicate).FirstOrDefault();
            if (driver != null)
                throw new Exception($"Driver with: '{driver.LicenceNumber}' licence number already exists");
            driver = new Driver(driverDTO.DriverId, driverDTO.FirstName, driverDTO.LastName, driverDTO.LicenceNumber, driverDTO.FreeMinutes, this._domainEventPublisher);
            this._unitOfWork.DriverRepository.Insert(driver);
            this._unitOfWork.Commit();
        }

        public List<DriverDTO> GetAllDrivers()
        {
            IList<Driver> drivers = this._unitOfWork.DriverRepository.GetAll()
                ?? throw new Exception("Could not find drivers");
            List<DriverDTO> result = this._driverMapper.Map(drivers);
            return result;
        }
    }
}
