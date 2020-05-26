﻿using System;
using System.Collections.Generic;
using System.Text;
using DDD.Base.ApplicationLayer.Services;
using DDD.CarRentalLib.ApplicationLayer.DTOs;

namespace DDD.CarRentalLib.ApplicationLayer.Interfaces
{
    public interface ICarService: IApplicationService
    {
        void CreateCar(CarDTO carDTO);
        void ChangePositionOfCar(Guid carId, PositionDTO positionDTO);
        List<CarDTO> GetAllCars();
    }
}
