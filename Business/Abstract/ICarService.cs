using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        List<Car> GetAllCars();
        List<Car> GetCarsByBrandId(int id);
        List<Car> GetCarsByColorId(int id);
        Car GetById(int id);
    }
}
