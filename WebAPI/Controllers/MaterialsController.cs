using Business.Abstract;
using Business.Utilities.CostsCurrencyCalculation;
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
    public class MaterialsController : ControllerBase
    {
        IMaterialService _materialService;
        public MaterialsController(IMaterialService materialService)
        {
            _materialService = materialService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllProduct()
        {
            var result = _materialService.GetAllMaterial();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _materialService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("Add")]
        public IActionResult Add(Material material)
        {
            var result = _materialService.Add(material);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(Material material)
        {
            var result = _materialService.Update(material);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(Material material)
        {
            var result = _materialService.Delete(material);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
