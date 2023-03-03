using Business.Abstract;
using Business.BusinessAspects.Autofac;
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
        public IResult Add(AccessoryPackageDetail accessoryPackageDetail)
        {
            if (accessoryPackageDetail != null)
            {
                _accessoryPackageDetailDal.Add(accessoryPackageDetail);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        [SecuredOperation("admin")]
        public IResult Delete(AccessoryPackageDetail accessoryPackageDetail)
        {
            if (accessoryPackageDetail != null)
            {
                _accessoryPackageDetailDal.Delete(accessoryPackageDetail);
                return new SuccessResult();
            }
            return new ErrorResult();
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
        public IResult Update(AccessoryPackageDetail accessoryPackageDetail)
        {
            if (accessoryPackageDetail != null)
            {
                _accessoryPackageDetailDal.Update(accessoryPackageDetail);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
