using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCar : ICarDal
    {
        List<Car> _cars;

        public InMemoryCar()
        {
            // Brand(Marka)
            _cars = new List<Car>
            {
                new Car {Id=1, BrandId=1, ColorId=1, ModelYear= new DateTime(2000,01,01), Description="2000 Model BMW Araba", DailyPrice= 90000},
                new Car {Id=2, BrandId=1, ColorId=1, ModelYear= new DateTime(2005,01,01), Description="2005 Model Mercedes Araba", DailyPrice= 120000},
                new Car {Id=3, BrandId=1, ColorId=1, ModelYear= new DateTime(2010,01,01), Description="2010 Model Fiat Araba", DailyPrice= 60000},
                new Car {Id=4, BrandId=1, ColorId=1, ModelYear= new DateTime(2007,01,01), Description="2007 Model Fiat Araba", DailyPrice= 30000}
            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carDelete = _cars.FirstOrDefault(x => x.Id == car.Id);
        }

        public List<Car> GetAllCars()
        {
            return _cars;
        }

        public List<Car> GetById(int id)
        {
            return _cars.Where(x => x.Id == id).ToList();
        }

        public void Update(Car car)
        {
            Car carUpdate = _cars.FirstOrDefault(x => x.Id == car.Id);
            carUpdate.ColorId = car.ColorId;
            carUpdate.BrandId = car.BrandId;
            carUpdate.Description = car.Description;
            carUpdate.DailyPrice = car.DailyPrice;
            carUpdate.ModelYear = car.ModelYear;
        }
    }
}
