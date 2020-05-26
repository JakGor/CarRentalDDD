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
    public class PassengerService : IPassengerService
    {
        private ICarRentalUnitOfWork _unitOfWork;
        private PassengerMapper _passengerMapper;
        private IDomainEventPublisher _domainEventPublisher;
        public PassengerService(ICarRentalUnitOfWork unitOfWork,
            PassengerMapper passengerMapper,
            IDomainEventPublisher domainEventPublisher)
        {
            this._unitOfWork = unitOfWork;
            this._passengerMapper = passengerMapper;
            this._domainEventPublisher = domainEventPublisher;
        }
        public void CreatePassenger(PassengerDTO passengerDTO)
        {
            Expression<Func<Passenger, bool>> expressionPredicate = x => x.Id == passengerDTO.PassengerId;
            Passenger passenger = this._unitOfWork.PassengerRepository.Find(expressionPredicate).FirstOrDefault();
            if (passenger != null)
                throw new Exception($"Passenger with Id: {passengerDTO.PassengerId} already exists");
            passenger = new Passenger(passengerDTO.PassengerId, passengerDTO.FirstName, passengerDTO.LastName,
                passengerDTO.CurrentPosition.X, passengerDTO.CurrentPosition.Y, this._domainEventPublisher);
            this._unitOfWork.PassengerRepository.Insert(passenger);
            this._unitOfWork.Commit();
        }

        public List<PassengerDTO> GetAllPassengers()
        {
            IList<Passenger> passengers = this._unitOfWork.PassengerRepository.GetAll()
                ?? throw new Exception("Could not find passengers");
            List<PassengerDTO> result = this._passengerMapper.Map(passengers);
            return result;

        }

        public void SetTarget(Guid passengerId, string targetAddressStreet, string targetAddressNumber, decimal moneyAmount)
        {
            Passenger passenger = this._unitOfWork.PassengerRepository.Get(passengerId)
                ?? throw new Exception($"Could not find passenger with Id: {passengerId}.");
            passenger.SelectTarget(targetAddressStreet, targetAddressNumber, moneyAmount);
            this._unitOfWork.Commit();

        }
    }
}
