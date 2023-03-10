using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Dtos;
using Entities.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IUserOperationClaimDal : IEntityRepository<UserOperationClaim>
    {
    }
}
