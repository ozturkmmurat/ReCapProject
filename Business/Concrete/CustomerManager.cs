using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerdal)
        {
            _customerDal = customerdal;
        }
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
          return  new SuccessResult(Messages.DataAdded);
        }

        public IResult Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.DataDeleted);
        }

        public IDataResult<List<Customer>> GetAllCustomer()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.GetByAll);
        }
        
        public IDataResult<Customer> GetById(int id)
        {
            var result = _customerDal.GetById(c => c.Id == id);
            if(result != null)
            {
                return new SuccessDataResult<Customer>(result, Messages.GetByIdMessage);
            }
             return new ErrorDataResult<Customer>(Messages.GetByAllDefault);
        }
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult(Messages.DataUpdate);
        }
    }
}
