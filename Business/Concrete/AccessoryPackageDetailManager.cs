using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation.AccessoryPackage;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AccessoryPackageDetailManager : IAccessoryPackageDetailService
    {
        IAccessoryPackageDetailDal _accessoryPackageDetailDal;
        public AccessoryPackageDetailManager(IAccessoryPackageDetailDal accessoryPackageDetailDal)
        {
            _accessoryPackageDetailDal = accessoryPackageDetailDal;
        }
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(AccessoryPackageDetailValidator))]
        public IResult Add(AccessoryPackageDetail accessoryPackageDetail)
        {
            if (accessoryPackageDetail != null)
            {
                _accessoryPackageDetailDal.Add(accessoryPackageDetail);
                return new SuccessResult(Messages.DataAdded);
            }
            return new ErrorResult(Messages.UnDataAdded);
        }
        [SecuredOperation("admin")]
        public IResult Delete(AccessoryPackageDetail accessoryPackageDetail)
        {
            if (accessoryPackageDetail != null)
            {
                _accessoryPackageDetailDal.Delete(accessoryPackageDetail);
                return new SuccessResult(Messages.DataDeleted);
            }
            return new ErrorResult(Messages.UnDataDeleted);
        }

        public IDataResult<List<AccessoryPackageDetail>> GetAllAccessoryPackage()
        {
            var result = _accessoryPackageDetailDal.GetAll();
            if (result != null )
            {
                return new SuccessDataResult<List<AccessoryPackageDetail>>(result);
            }
            return new ErrorDataResult<List<AccessoryPackageDetail>>();
        }

        public IDataResult<List<AccessoryPackageDetailDto>> GetAllAccessoryPackageDto()
        {
            var result = _accessoryPackageDetailDal.GetAllModelAccessoryDetailDto();
            if (result != null)
            {
                return new SuccessDataResult<List<AccessoryPackageDetailDto>>(result);
            }
            return new ErrorDataResult<List<AccessoryPackageDetailDto>>();
        }

        public IDataResult<List<AccessoryPackageDetailDto>> GetAllAccessoryPackageDtoById(int id)
        {
            var result = _accessoryPackageDetailDal.GetAllModelAccessoryDetailDto(x => x.AccessoryPackageDetailAccessoryPackageId == id);
            if (result != null )
            {
                return new SuccessDataResult<List<AccessoryPackageDetailDto>>(result);
            }
            return new ErrorDataResult<List<AccessoryPackageDetailDto>>();
        }

        public IDataResult<AccessoryPackageDetailDto> GetByAccessoryPackageDetail(int id)
        {
            var result = _accessoryPackageDetailDal.GetAccessoryPackageDetailById(x => x.AccessoryPackageDetailId == id);
            if (result != null)
            {
                return new SuccessDataResult<AccessoryPackageDetailDto>(result);
            }
            return new ErrorDataResult<AccessoryPackageDetailDto>();
        }

        public IDataResult<AccessoryPackageDetail> GetById(int id)
        {
            var result = _accessoryPackageDetailDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<AccessoryPackageDetail>(result);
            }
            return new ErrorDataResult<AccessoryPackageDetail>();
        }
        [SecuredOperation("admin")]
        [ValidationAspect(typeof(AccessoryPackageDetailValidator))]
        public IResult Update(AccessoryPackageDetail accessoryPackageDetail)
        {
            if (accessoryPackageDetail != null)
            {
                _accessoryPackageDetailDal.Update(accessoryPackageDetail);
                return new SuccessResult(Messages.DataUpdate);
            }
            return new ErrorResult(Messages.UnDataUpdate);
        }
    }
}
