using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICreditCartService
    {
        IResult Payment(int amount);
        IResult Add(CreditCard creditCard);
        IResult Delete(CreditCard creditCard);
        IDataResult<List<CreditCard>> GetAllCreditCard();
        IDataResult<CreditCard> GetById(int id);
        IDataResult<List<CreditCard>> GetByUserId(int id);
    }
}
