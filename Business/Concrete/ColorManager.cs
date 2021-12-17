using Business.Abstract;
using Business.Constans;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IResult Add(Color color)
        {
            _colorDal.Add(color);
            return new  SuccessResult(Messages.DataAdded);
        }


        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.DataDeleted);
        }

        public  IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), Messages.GetByAll);
        }

        public IDataResult<Color> GetById(int id)
        {
            var result = _colorDal.GetById(c => c.Id == id);
            if(result != null)
            {
                return new SuccessDataResult<Color>(result, "Veri başarıyla bulundu");
            }
            return new ErrorDataResult<Color>(result, Messages.GetByIdMessage);
            
        }

        public IDataResult<List<Color>> GetByName(string name)
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(c => c.Name == name), Messages.GetByAll);
        }

        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult(Messages.DataUpdate);
        }
    }
}
