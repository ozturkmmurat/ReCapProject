using Core.Utilities.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IFindexScoreService
    {
        IResult GetUserFindexScore();
    }
}
