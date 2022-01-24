using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
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
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
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

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.DataDeleted);
        }
        public IDataResult<List<Car>> GetAllCars()
        {

            return  new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.GetByAll);
        }

        public IDataResult<Car> GetById(int id)
        {
            var result = _carDal.Get(c => c.Id == id);
            if(result != null)
            {
                return new SuccessDataResult<Car>(result, Messages.GetByIdMessage);
            }
            return new ErrorDataResult<Car>(Messages.GetByAllDefault);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id), Messages.GetByAll);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id), Messages.GetByAll);
        }

        public IDataResult<List<CarDetailDTO>> GetCarsDetailDTO()
        {
           return new SuccessDataResult<List<CarDetailDTO>>(_carDal.GetCarDetails(), Messages.GetByAll);
        }
        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
           
            _carDal.Update(car);
            return new SuccessResult(Messages.DataUpdate);
        }

       
    }
}
