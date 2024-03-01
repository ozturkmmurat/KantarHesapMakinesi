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
    public class ProductsController : ControllerBase
    {
        IProductService _productCostService;
        public ProductsController(IProductService productCostService)
        {
            _productCostService = productCostService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllProduct()
        {
                var result = _productCostService.GetAllProduct();

                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
        }

        [HttpGet("GetAllDto")]
        public IActionResult GetAllProductDto()
        {
            var result = _productCostService.GetAllProductDto();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _productCostService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("TsaAdd")]
        public IActionResult TsaAdd(CRUDProductDto crudProductDto)
        {
            var result = _productCostService.TsaAdd(crudProductDto);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("TsaUpdate")]
        public IActionResult Update(CRUDProductDto crudProductDto)
        {
            var result = _productCostService.TsaUpdate(crudProductDto);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(Product product)
        {
            var result = _productCostService.Delete(product);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
