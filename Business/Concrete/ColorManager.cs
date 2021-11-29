﻿using Business.Abstract;
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
        public List<Color> GetAll()
        {
            return _colorDal.GetAll();
        }

        public Color GetById(int id)
        {
            return _colorDal.GetById(c=> c.Id == c.Id);
        }

        public List<Color> GetByName(string name)
        {
            return _colorDal.GetAll(c => c.Name == name);
        }
    }
}
