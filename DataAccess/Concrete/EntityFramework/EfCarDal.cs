using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapProjectContext>, ICarDal
    {
        public List<CarDetailDTO> GetCarsDetailsByBrandId(int brandId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Cars
                             join cl in context.Colors
                             on c.ColorId equals cl.Id
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join img in context.CarImages
                             on c.Id equals img.CarId
                             where b.Id == brandId
                             select new CarDetailDTO
                             {
                                 CarId = c.Id,
                                 CarName = c.Description,
                                 BrandName = b.Name,
                                 ColorName = cl.Name,
                                 DailyPrice = c.DailyPrice,
                                 ImagePath = img.ImagePath

                             };
                return result.ToList();
            }
        }

        public List<CarDetailDTO> GetByBrandNameByColorNameCarDetails(Expression<Func<CarDetailDTO, bool>> filter = null)
        {

            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join cl in context.Colors
                             on c.ColorId equals cl.Id
                             join img in context.CarImages
                             on c.Id equals img.CarId
                             select new CarDetailDTO
                             {
                                 CarId = c.Id,
                                 CarName = c.Description,
                                 BrandName = b.Name,
                                 ColorName = cl.Name,
                                 DailyPrice = c.DailyPrice,
                                 ImagePath = img.ImagePath
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();

            }
        }
        public List<CarDetailDTO> GetCarDetails()
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join cl in context.Colors
                             on c.ColorId equals cl.Id
                             select new CarDetailDTO
                             {
                                 CarId = c.Id,
                                 CarName = c.Description,
                                 BrandName = b.Name,
                                 ColorName = cl.Name,
                                 DailyPrice = c.DailyPrice,
                                 ModelYear = c.ModelYear
                             };
                return result.ToList();

            }
        }

        public CarDetailDTO GetCarIdDetails(Expression<Func<CarDetailDTO, bool>> filter)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Cars
                             join cl in context.Colors
                             on c.ColorId equals cl.Id
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join img in context.CarImages
                             on c.Id equals img.CarId
                             into colortemp
                             from img in colortemp.DefaultIfEmpty()
                             join r in context.Rentals
                             on c.Id equals r.CarId
                             into rentaltemp 
                             from r in rentaltemp.DefaultIfEmpty()
                             select new CarDetailDTO
                             {
                                 CarId = c.Id,
                                 CarName = c.Description,
                                 BrandName = b.Name,
                                 ColorName = cl.Name,
                                 DailyPrice = c.DailyPrice,
                                 ImagePath = img.ImagePath,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate,
                                 ColorId = cl.Id,
                                 BrandId = b.Id
                                 
                             };
                return result.FirstOrDefault(filter);
            }
        }

        public List<CarDetailDTO> GetCarsDetailsByColorId(int colorId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Cars
                             join cl in context.Colors
                             on c.ColorId equals cl.Id
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join img in context.CarImages
                             on c.Id equals img.CarId
                             where cl.Id == colorId
                             select new CarDetailDTO
                             {
                                 CarId = c.Id,
                                 CarName = c.Description,
                                 BrandName = b.Name,
                                 ColorName = cl.Name,
                                 DailyPrice = c.DailyPrice,
                                 ImagePath = img.ImagePath

                             };
                return result.ToList();
            }
        }
    }
}
