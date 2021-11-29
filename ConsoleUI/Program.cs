using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
           // carManager.Add(new Car { BrandId = 1, ColorId = 1, DailyPrice = 100000, Description = "2000 Model Siyah BMW ", ModelYear = new DateTime(2000, 01, 01) });
           
           // Console.WriteLine(carManager.GetById(1).Description);

            foreach (var car in carManager.GetCarsByBrandId(1))
            {
                Console.WriteLine(car.Description);
            }

        }
    }
}
