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
    public class ElectronicsController : ControllerBase
    {
        IElectronicService _electronicService;
        public ElectronicsController(IElectronicService electronicService)
        {
            _electronicService = electronicService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllProduct()
        {
            var result = _electronicService.GetAllElectronic();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _electronicService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("Add")]
        public IActionResult Add(Electronic electronic)
        {
            var result = _electronicService.Add(electronic);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(Electronic electronic)
        {
            var result = _electronicService.Update(electronic);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(Electronic electronic)
        {
            var result = _electronicService.Delete(electronic);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
