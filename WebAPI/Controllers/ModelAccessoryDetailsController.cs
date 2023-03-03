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
    public class ModelAccessoryDetailsController : ControllerBase
    {
        IModelAccessoryDetailService _modelAccessoryDetailService;
        public ModelAccessoryDetailsController(IModelAccessoryDetailService modelAccessoryDetailService)
        {
            _modelAccessoryDetailService = modelAccessoryDetailService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllProduct()
        {
            var result = _modelAccessoryDetailService.GetAllModelAccessoryDetail();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("GetBy")]
        public IActionResult GetAllProduct(int id)
        {
            var result = _modelAccessoryDetailService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetAllModelAccesoryDetailDto")]
        public IActionResult GetAllModelAccesoryDetailDto()
        {
            var result = _modelAccessoryDetailService.GetAllModelAccessoryDetailDto();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetAllModelAccesoryDetailByModelId")]
        public IActionResult GetAllModelAccessoryDetailByModelId(int modelId)
        {
            var result = _modelAccessoryDetailService.GetAllModelAccessoryDetailDtoById(modelId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetModelAccesoryDetailDtoByModelId")]
        public IActionResult GetModelAccesoryDetailDtoByModelId(int modelId)
        {
            var result = _modelAccessoryDetailService.GetModelAccesoryDetailDtoByModelId(modelId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(ModelAccessoryDetail modelAccessoryDetail)
        {
            var result = _modelAccessoryDetailService.Add(modelAccessoryDetail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(ModelAccessoryDetail modelAccessoryDetail)
        {
            var result = _modelAccessoryDetailService.Update(modelAccessoryDetail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
