using Business.Abstract;
using Business.Abstract.ProductModelCostDetail;
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
    public class ProductModelCostDetailsController : ControllerBase
    {
        IProductModelCostDetailService _productModelCostDetailService;
        IProductModelCostDetailSelectListService _productModelCostDetailSelectListService;
        public ProductModelCostDetailsController(
            IProductModelCostDetailService productModelCostDetailService,
            IProductModelCostDetailSelectListService productModelCostDetailSelectListService
            )
        {
            _productModelCostDetailService = productModelCostDetailService;
            _productModelCostDetailSelectListService = productModelCostDetailSelectListService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllProductModelCostDetails()
        {
            var result = _productModelCostDetailService.GetAllInstallationCostDetail();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetAllProductModelCostDetailDtoByProductModelCostId")]
        public IActionResult GetAllProductModelCostDetailDtoByProductModelCostId(int productModelCostId)
        {
            var result = _productModelCostDetailSelectListService.GetAllProductModelCostDetailDtoByProductModelCostId(productModelCostId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _productModelCostDetailService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetProductModelCostDetailLocationModelId")]
        public IActionResult GetProductModelCostDetailLocationModelId(int locationId, int modelId)
        {
            var result = _productModelCostDetailService.GetProductModelCostDetailLocationModelId(locationId, modelId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(ProductModelCostDetail productModelCostDetail)
        {
            var result = _productModelCostDetailService.Add(productModelCostDetail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(ProductModelCostDetail productModelCostDetail)
        {
            var result = _productModelCostDetailService.Update(productModelCostDetail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(ProductModelCostDetail productModelCostDetail)
        {
            var result = _productModelCostDetailService.Delete(productModelCostDetail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
