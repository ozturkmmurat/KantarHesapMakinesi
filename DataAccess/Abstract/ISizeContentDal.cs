using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ISizeContentDal: IEntityRepository<SizeContent>
    {
        List<SizeContentDto> GetAllSizeContentDto(Expression<Func<SizeContentDto, bool>> filter = null);
        SizeContentDto GetBySizeContentDto(Expression<Func<SizeContentDto, bool>> filter);
    }
}
