using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            _userDal.Add(user);
           return new SuccessResult(Messages.DataAdded);
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.DataDeleted);
        }
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }
        public IDataResult<List<User>> GetAllUser()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.GetByAll);
        }

        public IDataResult<User> GetById(int id)
        {
            var result = _userDal.Get(u => u.Id == id);
            if(result != null)
            {
                return new SuccessDataResult<User>(result, Messages.GetByIdMessage);               
            }
            return new ErrorDataResult<User>(Messages.GetByAllDefault);
        }

        public IResult Update(UserForUpdateDto userForUpdateDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForUpdateDto.Password, out passwordHash, out passwordSalt);

 
            if (GetByMail(userForUpdateDto.Email) != null && GetById(userForUpdateDto.UserId).Data.Email != userForUpdateDto.Email)
            {
                return new ErrorResult("Böyle bir e-mail mevcut başka bir mail adresi giriniz");
            }

              var user = new User
              {
                  Id = userForUpdateDto.UserId,
                  Email = userForUpdateDto.Email,
                  FirstName = userForUpdateDto.FirstName,
                  LastName = userForUpdateDto.LastName,
                  PasswordHash = passwordHash,
                  PasswordSalt = passwordSalt,
                  Status = true
              };
            _userDal.Update(user);
            return new SuccessResult(Messages.DataUpdate);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }
    }
}
