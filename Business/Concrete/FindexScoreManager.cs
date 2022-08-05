using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class FindexScoreManager : IFindexScoreService
    {
        public IResult GetUserFindexScore()
        {
            Random carFindex = new Random();
            Random userFindex = new Random();
            var carFindexResult = carFindex.Next(0, 1900);
            var userFindexResult = userFindex.Next(0, 1900);

            if(userFindexResult > carFindexResult)
            {
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult("Findex puanınız aracı kiralamak için yetersiz");
            }

        }
    }
}
