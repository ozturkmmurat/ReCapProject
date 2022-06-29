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
        public IResult Payment(CreditCard creditCart, int amount)
        {
            var balance = new Random().Next(100, 6000);
            if (amount <= balance)
            {
                return new ErrorResult("Ödeme başarıyla gerçekleşti.");
            }
            return new ErrorResult("Yetersiz bakiye");
        }

    }
}
