using Business.Abstract;
using Business.Constans;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IResult Add(Car car)
        {    
           if (car.Description.Length <= 2)
                {
                return new ErrorResult(Messages.DataRuleFail);
                }
          else if (car.DailyPrice <= 0)
                {
                return new ErrorResult(Messages.DataRuleFail);
            }
            else
            {
                _carDal.Add(car);
                return new SuccessResult(Messages.DataAdded);
            }
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
            var result = _carDal.GetById(c => c.Id == id);
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

        public IResult Update(Car car)
        {
           
            _carDal.Update(car);
            return new SuccessResult(Messages.DataUpdate);
        }

       
    }
}
