using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entities.Dtos.User;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, KantarHesapMakinesiContext>, IUserDal
    {
        private readonly KantarHesapMakinesiContext _context;

        public EfUserDal(KantarHesapMakinesiContext context) : base(context)
        {
            _context = context;
        }

        public List<UserDto> GetAllUserDto(Expression<Func<UserDto, bool>> filter = null)
        {
                var result = from u in _context.Users
                             join uop in _context.UserOperationClaims
                             on u.Id equals uop.UserId
                             into userOperationClaimTemp
                             from uopt in userOperationClaimTemp.DefaultIfEmpty()
                             join op in _context.OperationClaims
                             on uopt.OperationClaimId equals op.Id
                              into operationClaimTemp
                             from opct in operationClaimTemp.DefaultIfEmpty()

                             select new UserDto
                             {
                                 UserOperationClaimId = uopt.Id,
                                 OperationClaimId = opct.Id,
                                 UserId = u.Id,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email,
                                 Status = u.Status,
                                 OperationClaimName = opct.Name
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
        }

        public List<OperationClaim> GetClaims(User user)
        {
                var result = from operationClaim in _context.OperationClaims
                             join userOperationClaim in _context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();
        }
    }
}
