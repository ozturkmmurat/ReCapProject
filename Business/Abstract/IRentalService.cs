using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAllRental();
        IDataResult<Rental> GetById(int id);
        IDataResult<List<RentalDetailsDto>> GetRentalDetailsDto();
        IResult Add(Rental rental, CreditCard creditCard, int amount);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);
    }
}
