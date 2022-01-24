using Core.Entities.Concrete;
using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<List<User>> GetAllUser();
        IDataResult<User> GetById(int id);
        User GetByMail(string email);
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);     

    }
}
