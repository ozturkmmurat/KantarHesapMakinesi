using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
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
    public class ProductModelCostsController : ControllerBase
    {
        IProductModelCostService _productModelCostService;
        public ProductModelCostsController(IProductModelCostService productModelCostService)
        {
            _productModelCostService = productModelCostService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllProductModelCosts()
        {
            var result = _productModelCostService.GetAllProductModelCost();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _productModelCostService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("GetProductModelCostDtoByModelId")]
        public IActionResult GetProductModelCostDtoByModelId(int modelId)
        {
            var result = _productModelCostService.GetProductModelCostDtoByModelId(modelId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(ProductModelCost productModelCost)
        {
            var result = _productModelCostService.Add(productModelCost);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("AddProductModelCost")]
        public IActionResult AddProductModelCost(ProductModelCostDto productModelCostDto)
        {
            var result = _productModelCostService.AddProductModelCost(productModelCostDto);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(ProductModelCost productModelCost)
        {
            var result = _productModelCostService.Update(productModelCost);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("UpdateProductModelCost")]
        public IActionResult UpdateProductModelCost(ProductModelCostDto productModelCostDto)
        {
            var result = _productModelCostService.UpdateProductModelCost(productModelCostDto);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(ProductModelCost productModelCost)
        {
            var result = _productModelCostService.Delete(productModelCost);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
