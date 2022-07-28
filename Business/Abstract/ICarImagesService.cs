using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarImagesService
    {
        IDataResult<List<CarImages>> GetAllCarsById(int id);
        IDataResult<List<CarImages>> GetAll();
        IDataResult<CarImages> GetCarsById(int id);
        IDataResult<CarImages> GetById(int id);
        IResult Add(IFormFile file,CarImages carImages);
        IResult Update(IFormFile file, CarImages carImages);
        IResult Delete(CarImages carImages);
    }
}
