﻿using Core.DataAccess;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICarDal: IEntityRepository<Car>
    {
        List<CarDetailDTO> GetCarDetails();
        CarDetailDTO GetCarIdDetails(Expression<Func<CarDetailDTO, bool>> filter);
        List<CarDetailDTO> GetByBrandNameByColorNameCarDetails(Expression<Func<CarDetailDTO, bool>> filter = null);
        List<CarDetailDTO> GetCarsDetailsByBrandId(int brandId);
        List<CarDetailDTO> GetCarsDetailsByColorId(int colorId);
    }
}
