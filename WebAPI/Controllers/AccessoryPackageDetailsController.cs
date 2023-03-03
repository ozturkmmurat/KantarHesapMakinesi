using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessoryPackageDetailsController : ControllerBase
    {
        IAccessoryPackageDetailService _accessoryPackageDetailService;
        public AccessoryPackageDetailsController(IAccessoryPackageDetailService accessoryPackageDetailService)
        {
            _accessoryPackageDetailService = accessoryPackageDetailService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllAccesory()
        {
            var result = _accessoryPackageDetailService.GetAllAccessoryPackage();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetAllAccesoryPackageDtoById")]
        public IActionResult GetAllAccesoryPackageDtoById(int id)
        {
            var result = _accessoryPackageDetailService.GetAllAccessoryPackageDtoById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetAllAccesoryPackageDto")]
        public IActionResult GetAllAccesoryPackageDto()
        {
            var result = _accessoryPackageDetailService.GetAllAccessoryPackageDto();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetAccesoryPackageDtoById")]
        public IActionResult GetAccesoryPackageDtoById(int id)
        {
            var result = _accessoryPackageDetailService.GetAllAccessoryPackageDtoById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _accessoryPackageDetailService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("Add")]
        public IActionResult Add(AccessoryPackageDetail accessoryPackageDetail)
        {
            var result = _accessoryPackageDetailService.Add(accessoryPackageDetail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(AccessoryPackageDetail accessoryPackageDetail)
        {
            var result = _accessoryPackageDetailService.Update(accessoryPackageDetail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(AccessoryPackageDetail accessoryPackageDetail)
        {
            var result = _accessoryPackageDetailService.Delete(accessoryPackageDetail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
