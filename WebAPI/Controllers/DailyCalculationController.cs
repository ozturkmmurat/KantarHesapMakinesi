using Business.Abstract.SP;
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
    public class DailyCalculationController : ControllerBase
    {
        IDailyCalculationService _dailyCalculationService;
        public DailyCalculationController(IDailyCalculationService dailyCalculationService)
        {
            _dailyCalculationService = dailyCalculationService;
        }


        [HttpPost("CalculateCostSP")]
        public IActionResult CalculateCostSp()
        {
            var result = _dailyCalculationService.CalculateCostSP();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
