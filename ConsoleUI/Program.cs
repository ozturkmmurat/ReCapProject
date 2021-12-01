﻿using Business.Concrete;
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
            // CarManager carManager = CarTest();
            //BrandManager brandManager = BrandTest();
            //ColorManager colorManager = ColorUpdateTest();
            //ColorDelete();
            //Console.WriteLine(carManager.GetById(1).Description);

            //foreach (var car in carManager.GetCarsByBrandId(1))
            //{
            //    Console.WriteLine(car.Description);
            //}

            //foreach (var item in brandManager.GetAll())
            //{
            //    Console.WriteLine(item.Id + "/" + item.Name);
            //}



            //foreach (var item in colorManager.GetAll())
            //{
            //    Console.WriteLine(item.Name);
            //}

            CarManager carManager = new CarManager(new EfCarDal());
            foreach (var item in carManager.GetCarsDetailDTO())
            {
                Console.WriteLine("Araç Adı = " + item.CarName + " -- Araç Markası = "
                    + item.BrandName + " -- Araç Rengi = " + item.ColorName +
                    " -- Araç Günlük Ücreti = " + item.DailyPrice);
            }
        }

        private static void ColorDelete()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());
            colorManager.Delete(new Color { Id = 1002 });
        }

        private static ColorManager ColorUpdateTest()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());
            colorManager.Update(new Color { Name = "Kapalı Mavi" });
            return colorManager;
        }


        private static CarManager CarTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            carManager.Add(new Car { BrandId = 1, ColorId = 1, DailyPrice = 100000, Description = "2000 Model Siyah BMW ", ModelYear = new DateTime(2000, 01, 01) });
            return carManager;
        }

        private static BrandManager BrandTest()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            brandManager.Add(new Brand { Name = "Citroen" });
            return brandManager;
        }
    }
}