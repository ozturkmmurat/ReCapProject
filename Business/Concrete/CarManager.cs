using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcers.Validation;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Core.Aspects.Autofac.Validation.ValidationAspect;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        [SecuredOperation("caradd,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            //var context = new ValidationContext<Car>(car); // Bir doğrulama contexti oluşturduk
            //CarValidator carValidator = new CarValidator();  
            //var result = carValidator.Validate(context); // CarValidator classındaki kurallara uyuyor mu context kontrol et

           /* ValidationTool.Validate(new CarValidator(), car); */ // Üstteki 3 satır kodun yaptığı işlemi Core katmanında 
            // CrossCutingConcers bölümünde gerçekleştirdik
                _carDal.Add(car);
                return new SuccessResult(Messages.DataAdded);

        }
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.DataDeleted);
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetAllCars()
        {

            return  new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.GetByAll);
        }
        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            var result = _carDal.Get(c => c.Id == id);
            if(result != null)
            {
                return new SuccessDataResult<Car>(result, Messages.GetByIdMessage);
            }
            return new ErrorDataResult<Car>(Messages.GetByAllDefault);
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id), Messages.GetByAll);
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id), Messages.GetByAll);
        }
        [CacheAspect]
        public IDataResult<List<CarDetailDTO>> GetCarsDetailDTO()
        {
           return new SuccessDataResult<List<CarDetailDTO>>(_carDal.GetCarDetails(), Messages.GetByAll);
        }
        public IDataResult<List<CarDetailDTO>> GetByBrandNameByColorNameCarDetails(string brandName, string colorName)
        {
            
            if (brandName == "undefined" && colorName != null)
            {
                return new SuccessDataResult<List<CarDetailDTO>>(_carDal.GetByBrandNameByColorNameCarDetails(x => x.ColorName == colorName));
            }
            else if (colorName == "undefined" && brandName != null)
            {
                return new SuccessDataResult<List<CarDetailDTO>>(_carDal.GetByBrandNameByColorNameCarDetails(x => x.BrandName == brandName));
            }
            return new SuccessDataResult<List<CarDetailDTO>>(_carDal.GetByBrandNameByColorNameCarDetails(x => x.BrandName == brandName && x.ColorName == colorName));
        }
        public IDataResult<CarDetailDTO> GetCarsIdDetailDTO(int id)
        {
            var result = _carDal.GetCarIdDetails(c => c.CarId == id);
            if (result != null)
            {
                return new SuccessDataResult<CarDetailDTO>(result, Messages.GetByAll);
            }
            else
            {
                return new ErrorDataResult<CarDetailDTO>(Messages.GetByAllDefault);
            }
        }

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
           
            _carDal.Update(car);
            return new SuccessResult(Messages.DataUpdate);
        }

        public IDataResult<List<CarDetailDTO>> GetCarDetailsByBrandId(int brandId)
        {
            if (brandId !=0)
            {
                return new SuccessDataResult<List<CarDetailDTO>>(_carDal.GetCarsDetailsByBrandId(brandId));
            }
            return new ErrorDataResult<List<CarDetailDTO>>("Veriler listelenemedi.");
        }

        public IDataResult<List<CarDetailDTO>> GetCarDetailsByColorId(int colorId)
        {
            if (colorId != 0)
            {
                return new SuccessDataResult<List<CarDetailDTO>>(_carDal.GetCarsDetailsByColorId(colorId));
            }
            return new ErrorDataResult<List<CarDetailDTO>>("Veriler listelenemedi.");
        }
    }
}
