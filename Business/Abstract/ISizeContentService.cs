using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ISizeContentService
    {
        IDataResult<List<SizeContent>> GetAllSizeContent();
        IDataResult<List<SizeContentDto>> GetAllSizeCtDtoBySizeId(int sizeId);
        IDataResult<SizeContent> GetById(int id);
        IResult Add(SizeContent modelHeightWeight);
        IResult Update(SizeContent modelHeightWeight);
        IResult Delete(SizeContent modelHeightWeight);
    }
}
