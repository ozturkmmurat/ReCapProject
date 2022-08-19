using Business.Abstract;
using Business.BusinessAspects.Autofac;
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
            if (result != null)
            {
                return new SuccessDataResult<User>(result, Messages.GetByIdMessage);
            }
            return new ErrorDataResult<User>(Messages.GetByAllDefault);
        }
        [SecuredOperation("user")]
        public IResult Update(UserForUpdateDto userForUpdateDto)
        {
            byte[] passwordHash, passwordSalt;


            if (GetByMail(userForUpdateDto.Email) != null && GetById(userForUpdateDto.UserId).Data.Email != userForUpdateDto.Email)
            {
                return new ErrorResult("Böyle bir e-mail mevcut başka bir mail adresi giriniz");
            }


            if (userForUpdateDto.NewPassword != null)
            {
                if(userForUpdateDto.OldPassword == null)
                {
                    return new ErrorResult("Şifrenizi yenilemek içni eski şifreyi de girmelisiniz");
                }
                var result = CheckPassword(userForUpdateDto.Email, userForUpdateDto.OldPassword);
                if (result.Success != true)
                {
                    return new ErrorResult("Eski şifreniz hatalı");
                }
                HashingHelper.CreatePasswordHash(userForUpdateDto.NewPassword, out passwordHash, out passwordSalt);
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
            }
            else
            {

                var result = GetById(userForUpdateDto.UserId);
                var user = new User
                {
                    Id = userForUpdateDto.UserId,
                    Email = userForUpdateDto.Email,
                    FirstName = userForUpdateDto.FirstName,
                    LastName = userForUpdateDto.LastName,
                    PasswordHash = result.Data.PasswordHash,
                    PasswordSalt = result.Data.PasswordSalt,
                    Status = true
                };
                _userDal.Update(user);
            }


            return new SuccessResult(Messages.DataUpdate);
        }

        public User GetByMail(string email)
        {
            var result = _userDal.Get(u => u.Email == email);
           if (result !=null)
            {
                return result;
            }
            return null;
        }

        public IDataResult<User> GetWhereMailById(int id)
        {
            var result = _userDal.Get(u => u.Id == id);
            return new SuccessDataResult<User>(result);
        }

        public IResult CheckPassword(string email, string password)
        {
            var userToCheck = GetByMail(email);
            if(userToCheck != null)
            {
                if (!HashingHelper.VerifyPasswordHash(password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
                {
                    return new ErrorDataResult<User>();
                }
            }
            return new SuccessResult();
        }

        public IResult CheckEmail(string email)
        {
            var userToCheck = GetByMail(email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>();
            }
            return new SuccessResult();
        }

        public IResult UpdateRefreshToken(string refreshToken,User user, DateTime accessTokenDate)
        {
            if(user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddMinutes(10);
                _userDal.Update(user);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IDataResult<User> GetByRefreshToken(string refreshToken)
        {
            var result = _userDal.Get(u => u.RefreshToken == refreshToken);
            if(result != null)
            {
                return new SuccessDataResult<User>(result);
            }
            return new ErrorDataResult<User>();
        }
    }
}
