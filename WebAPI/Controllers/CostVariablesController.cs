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
    public class CostVariablesController : ControllerBase
    {
        ICostVariableService _costVariableService;
        public CostVariablesController(ICostVariableService costVariableService)
        {
            _costVariableService = costVariableService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllCostVariable()
        {
            var result = _costVariableService.GetAllCostVariable();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _costVariableService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("Add")]
        public IActionResult Add(CostVariable costVariable,  decimal xValue,  decimal yValue)
        {
            var result = _costVariableService.Add(costVariable, xValue, yValue);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(CostVariable costVariable, decimal xValue, decimal yValue)
        {
            var result = _costVariableService.Update(costVariable, xValue, yValue);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(CostVariable costVariable)
        {
            var result = _costVariableService.Delete(costVariable);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
