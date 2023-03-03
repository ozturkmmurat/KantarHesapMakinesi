using Business.Abstract;
using Core.Utilities.ExchangeRate.CurrencyGet;
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
    public class AccessoriesController : ControllerBase
    {
        IAccessoryService _accessoryService;
        public AccessoriesController(IAccessoryService accessoryService)
        {
            _accessoryService = accessoryService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllAccesory()
        {
            var result = _accessoryService.GetAllAccessory();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _accessoryService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            var result = _accessoryService.GetByName(name);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("Add")]
        public IActionResult Add(Accessory accessory)
        {
            var result = _accessoryService.Add(accessory);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(Accessory accessory)
        {
            var result = _accessoryService.Update(accessory);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(Accessory accessory)
        {
            var result = _accessoryService.Delete(accessory);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
