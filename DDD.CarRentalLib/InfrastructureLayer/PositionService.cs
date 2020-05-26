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
    public class PositionService: IDomainService
    {
        private ICarRentalUnitOfWork _unitOfWork;        
        private IDomainEventPublisher _domainEventPublisher;
        public PositionService(ICarRentalUnitOfWork unitOfWork,
            
            IDomainEventPublisher domainEventPublisher)
        {
            this._unitOfWork = unitOfWork;
            
            this._domainEventPublisher = domainEventPublisher;
        }

        public void CarPosition(Car car)
        {
            Random rnd = new Random();
            Position newPosition = new Position(rnd.Next(-10,10), rnd.Next(-10, 10));
            car.ChangePosition(newPosition);
            this._unitOfWork.Commit();
        }
        
       
    }
}
