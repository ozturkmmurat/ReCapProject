using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.Helpers.FileHelper;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarImagesManager : ICarImagesService
    {
        ICarImagesDal _carsImages;
        IFileHelper _fileHelper;
        public CarImagesManager(ICarImagesDal carImages, IFileHelper fileHelper)
        {
            _carsImages = carImages;
            _fileHelper = fileHelper;
        }

        public IResult Add(IFormFile file, CarImages carImages)
        {
            IResult result = BusinessRules.Run(CheckIfCarImageLimit(carImages.Id));
            if(result != null)
            {
                return new ErrorResult();
            }
            carImages.ImagePath = _fileHelper.Upload(file, PathConstans.ImagesPath);
            carImages.CreateDate = DateTime.Now;
            _carsImages.Add(carImages);
            return new SuccessResult(Messages.DataAdded);
        }

        public IResult Delete(CarImages carImages)
        {
            _fileHelper.Delete(PathConstans.ImagesPath + carImages.ImagePath);
            _carsImages.Delete(carImages);
            return new SuccessResult();
        }

        public IDataResult<List<CarImages>> GetAll()
        {
            return new SuccessDataResult<List<CarImages>>(_carsImages.GetAll());
        }

        public IDataResult<CarImages> GetById(int id)
        {
            var result = _carsImages.Get(c => c.Id == id);
            if(result != null)
            {
                return new SuccessDataResult<CarImages>(result, Messages.GetByIdMessage);
            }
            return new ErrorDataResult<CarImages>(Messages.GetByAllDefault);
        }

        public IDataResult<List<CarImages>> GetCarsById(int id)
        {
            IResult result = BusinessRules.Run(CheckCarImage(id));
            if(result != null)
            {
                return new ErrorDataResult<List<CarImages>>(GetDefaultCarImage(id).Data);
            }
            return new SuccessDataResult<List<CarImages>>(_carsImages.GetAll(c => c.Id == id));
        }

        public IResult Update(IFormFile file, CarImages carImages)
        {
            carImages.ImagePath = _fileHelper.Update(file, PathConstans.ImagesPath+ carImages.ImagePath, PathConstans.ImagesPath);  // Dosya işlemlerinde silme ve tekrar oluşturma için gereken parametreler 
            _carsImages.Update(carImages); // Veritabanında güncellemek için  adını veriyoruz
            return new SuccessResult();

        }
        private IResult CheckIfCarImageLimit(int id)
        {
            var result = _carsImages.GetAll(c => c.CarId == id).Count;
            if(result >= 5)
            {
                return new ErrorResult(Messages.CheckIfCarImageLimit);
            }
            return new SuccessResult();
        }
        private IDataResult<List<CarImages>> GetDefaultCarImage(int id)
        {
            List<CarImages> carImages = new List<CarImages>();
            carImages.Add(new CarImages {CarId = id, ImagePath = "DefaultImage.jpg" });
            return new  SuccessDataResult<List<CarImages>>(carImages);
        }
        private IResult CheckCarImage(int carId)
        {
            var result = _carsImages.GetAll(c => c.CarId == carId).Count;
            if (result > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
