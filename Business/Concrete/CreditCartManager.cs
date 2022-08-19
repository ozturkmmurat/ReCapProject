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
    public class CreditCartManager : ICreditCartService
    {
        ICreditCartDal _creditCart;
        public CreditCartManager(ICreditCartDal creditCart)
        {
            _creditCart = creditCart;
        }

        public IResult Add(CreditCard creditCard)
        {
            if(creditCard != null)
            {
               _creditCart.Add(creditCard);
                return new SuccessResult("Kredi kartınız kaydedildi.");  
            }
            return new ErrorResult("Kredi kartınız kaydedilemedi.");
        }

        public IResult Delete(CreditCard creditCard)
        {
            if(creditCard != null)
            {
                _creditCart.Delete(creditCard);
                return new SuccessResult("Kredi kartınız başarıyla silinmiştir.");
            }
            return new ErrorResult("Kredi kartı silinirken bir sorun oluştu.");
        }

        public IDataResult<List<CreditCard>> GetAllCreditCard()
        {
            var result = _creditCart.GetAll();
            if(result != null)
            {
                return new SuccessDataResult<List<CreditCard>>(result, Messages.GetCreditCard);
            }
            return new ErrorDataResult<List<CreditCard>>(Messages.GetDefaultCreditCard);
        }

        public IDataResult<CreditCard> GetById(int id)
        {
            var result = _creditCart.Get(x => x.Id == id);
            if(result != null)
            {
                return new SuccessDataResult<CreditCard>(result);
            }
            return new ErrorDataResult<CreditCard>();
        }

        public IDataResult<List<CreditCard>> GetByUserId(int id)
        {
            var result = _creditCart.GetAll(x => x.UserId == id);
            if(result != null)
            {
                return new SuccessDataResult<List<CreditCard>>(result, Messages.GetCreditCard);
            }
            return new ErrorDataResult<List<CreditCard>>(Messages.GetDefaultCreditCard);
        }

        public IResult Payment(int amount)
        {
            var balance = new Random().Next(100, 6000);
            if (balance >= amount)
            {
                return new SuccessResult("Ödeme başarıyla gerçekleşti.");
            }
            return new ErrorResult("Yetersiz bakiye");
        }

    }
}
