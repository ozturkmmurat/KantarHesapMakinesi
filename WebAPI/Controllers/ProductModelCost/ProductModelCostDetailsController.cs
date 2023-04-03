using Business.Abstract.ProductModelCost;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.ProductModelCost
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductModelCostDetailsController : ControllerBase
    {
        IProductModelCostDetailService _productModelCostDetailService;
        public ProductModelCostDetailsController(IProductModelCostDetailService productModelCostDetailService)
        {
            _productModelCostDetailService = productModelCostDetailService;
        }

        [HttpGet("GetCalculate")]
        public IActionResult GetCalculate(int modelId, int installationCostLocationId, int accessoryId)
        {
            var result = _productModelCostDetailService.GetCalculate(modelId, installationCostLocationId, accessoryId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
