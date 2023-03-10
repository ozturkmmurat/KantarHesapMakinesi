using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        IOperationClaimDal _operationClaimDal;
        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }
        public IDataResult<List<OperationClaim>> GetAllOperationClaim()
        {
            var result = _operationClaimDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<OperationClaim>>(result);
            }
            return new ErrorDataResult<List<OperationClaim>>();
        }
    }
}
