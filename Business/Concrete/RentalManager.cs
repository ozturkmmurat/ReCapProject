using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        ICarService _carService;
        ICreditCartService _creditCartService;
        IFindexScoreService _findexScoreService;
        public RentalManager(IRentalDal rentalDal, ICarService carService, ICreditCartService creditCartService, IFindexScoreService findexScoreService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
            _creditCartService = creditCartService;
            _findexScoreService = findexScoreService;
        }
        public IResult Add(Rental rental, CreditCard creditCard, int amount)
        {
            IResult result = BusinessRules.Run(CheckIfCarRental(rental));
            if (result != null)
            {
                TimeSpan ts = rental.ReturnDate - rental.RentDate;
                var creditCartResult = _creditCartService.Payment(amount);
                var findexScoreResult = _findexScoreService.GetUserFindexScore();
                if (!creditCartResult.Success)
                {
                    creditCartResult.Message.ToString();

                }
                else if (!findexScoreResult.Success)
                {
                    creditCartResult.Message.ToString();
                }
                else
                {
                    _rentalDal.Add(rental);
                    return new SuccessResult(Messages.DataAdded);

                }
            }
            var informationResult = _rentalDal.Get(x => x.CarId == rental.CarId).ReturnDate;
            return new ErrorResult("Araç bu tarihe kadar kiralanmış " + informationResult);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.DataDeleted);
        }

        public IDataResult<List<Rental>> GetAllRental()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.GetByAll);
        }

        public IDataResult<Rental> GetById(int id)
        {
            var result = _rentalDal.Get(c => c.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Rental>(result, Messages.GetByIdMessage);
            }
            return new ErrorDataResult<Rental>(Messages.GetByAllDefault);
        }

        public IDataResult<List<RentalDetailsDto>> GetRentalDetailsDto()
        {
            return new SuccessDataResult<List<RentalDetailsDto>>(_rentalDal.GetRentalDetails(), Messages.GetRentalDetailsDto);
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.DataUpdate);
        }
        public IResult CheckIfCarRental(Rental rental)
        {
            var result = _rentalDal.GetAll(r => r.CarId == rental.CarId && rental.RentDate > r.ReturnDate && rental.ReturnDate > r.RentDate).Any();
            if (result)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
