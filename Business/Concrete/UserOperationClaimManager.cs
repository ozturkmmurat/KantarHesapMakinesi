using Business.Abstract.User;
using Business.Constans;
using Core.Entities.Concrete;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete.User
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        IUserOperationClaimDal _userOperationClaimDal;
        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        public IDataResult<List<UserOperationClaim>> GetAllUserOpeartionClaim()
        {
            var result = _userOperationClaimDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<UserOperationClaim>>(result);
            }
            return new ErrorDataResult<List<UserOperationClaim>>();
        }

        public IResult Update(UserOperationClaim userOperationClaim)
        {
            if(userOperationClaim != null)
            {
                _userOperationClaimDal.Update(userOperationClaim);
                return new SuccessResult(Messages.DataUpdate);
            }
            return new ErrorResult(Messages.UnDataUpdate);
        }
    }
}
