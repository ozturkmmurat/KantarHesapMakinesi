using Core.Entities.Concrete;
using Core.Utilities.Result.Abstract;
using Entities.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract.User
{
    public interface IUserOperationClaimService
    {
        IDataResult<List<UserOperationClaim>> GetAllUserOpeartionClaim();
        IResult Update(UserOperationClaim userOperationClaim);
    }
}
