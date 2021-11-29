using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public void Add(Car car)
        {    
           if (car.Description.Length <= 2)
                {
                    Console.WriteLine("Aracın açıklaması 2 karakterden az olamaz");
                }
          else if (car.DailyPrice <= 0)
                {
                    Console.WriteLine("Aracın fiyatı 0 dan düşük olamaz");
                }
            else
            {
                _carDal.Add(car);
            }
        } 

        public List<Car> GetAllCars()
        {
            return _carDal.GetAll();
        }

        public Car GetById(int id)
        {
            return _carDal.GetById(c => c.Id == id);
        }

        public List<Car> GetCarsByBrandId(int id)
        {
            return _carDal.GetAll(c => c.BrandId == id);
        }

        public List<Car> GetCarsByColorId(int id)
        {
            return _carDal.GetAll(c => c.ColorId == id);
        }
    }
}
