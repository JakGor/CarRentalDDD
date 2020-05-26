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
using DDD.Base.DomainModelLayer.Models;
using DDD.Base.DomainModelLayer.Services;

namespace DDD.CarRentalLib.InfrastructureLayer
{
    public class HandlePassengerService: IDomainService
    {
        private ICarRentalUnitOfWork _unitOfWork;
        private PassengerMapper _passengerMapper;
        private IDomainEventPublisher _domainEventPublisher;
        public HandlePassengerService(ICarRentalUnitOfWork unitOfWork,
            PassengerMapper passengerMapper,

            IDomainEventPublisher domainEventPublisher)
        {
            this._unitOfWork = unitOfWork;
            this._passengerMapper = passengerMapper;
            this._domainEventPublisher = domainEventPublisher;
        }

        public Dictionary<PassengerDTO,Distance> ShowPassengersWithDistance(Car car)
        {
            IList<Passenger> passengers = this._unitOfWork.PassengerRepository.GetAll();
            List<PassengerDTO> passengerDTOs = this._passengerMapper.Map(passengers);
            Dictionary<PassengerDTO, Distance> result = new Dictionary<PassengerDTO, Distance>();
            foreach(PassengerDTO p in passengerDTOs)
            {
                if(p.Status == PassengerStatusDTO.Free)
                {
                    Position currentPassengerPosition = new Position(p.CurrentPosition.X, p.CurrentPosition.Y);
                    Distance distanceToPassenger = car.CurrentPosition.CalculateDistance(car.CurrentPosition, currentPassengerPosition);
                    result.Add(p, distanceToPassenger);
                }

            }
            return result;
        }
        public void ReservePassenger(Passenger passenger)
        {
            if (passenger.Status == PassengerStatus.Free)
                passenger.PassengerReserved();
            else
                throw new Exception("Passenger have not selected target or is already reserved or taken");
        }
        public void TakePassenger(Car car, Passenger passenger)
        {
            if (car.CurrentPosition.Unit == passenger.CurrentPosition.Unit &&
                car.CurrentPosition.X == passenger.CurrentPosition.X &&
                car.CurrentPosition.Y == passenger.CurrentPosition.Y)
            {
                passenger.PassengerTaken();
            }
            else
                throw new Exception("Impossible to take passenger - different positions");
        }

    }
}
