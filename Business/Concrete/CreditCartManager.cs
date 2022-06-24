using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CreditCartManager : ICreditCartService
    {
        ICreditCartDal _creditCart;
        public CreditCartManager(ICreditCartDal creditCart)
        {
            _creditCart = creditCart;
        }
        public IResult Payment(CreditCard creditCart)
        {
            _creditCart.Add(creditCart);
            return new SuccessResult("Ödeme başarıyla gerçekleşti.");
            
        }

    }
}
