﻿using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IModelService
    {
        IDataResult<List<Model>> GetAllModel();
        IDataResult<List<ModelDto>> GetAllModelDto();
        IDataResult<Model> GetById(int id);
        IDataResult<List<Model>> GetAllByProductId(int productId);
        IDataResult<ModelDto> GetModelDtoById(int id);
        IResult AddDto(ModelDto modelDto);
        IResult UpdateDto(ModelDto modelDto);
        IResult Update(Model model);
        IResult Delete(Model model);
        ModelDto ModelInFormation(ModelDto modelDto);
    }
}
