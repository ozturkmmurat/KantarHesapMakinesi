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
    public class AccessoryPackagesController : ControllerBase
    {
        IAccessoryPackageService _accessoryPackageService;
        public AccessoryPackagesController(IAccessoryPackageService accessoryPackageService)
        {
            _accessoryPackageService = accessoryPackageService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllAccesoryPackage()
        {
            var result = _accessoryPackageService.GetAllAccessoryPackage();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _accessoryPackageService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("Add")]
        public IActionResult Add(AccessoryPackage accessoryPackage)
        {
            var result = _accessoryPackageService.Add(accessoryPackage);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(AccessoryPackage accessoryPackage)
        {
            var result = _accessoryPackageService.Update(accessoryPackage);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(AccessoryPackage accessoryPackage)
        {
            var result = _accessoryPackageService.Delete(accessoryPackage);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
