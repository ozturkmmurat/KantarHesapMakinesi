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
    public class ModelElectronicDetailsController : ControllerBase
    {
        private IModelElectronicDetailService _modelElectronicDetailService;
        public ModelElectronicDetailsController(IModelElectronicDetailService modelElectronicDetailService)
        {
            _modelElectronicDetailService = modelElectronicDetailService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _modelElectronicDetailService.GetAllModelElectronicDetail();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("GetBy")]
        public IActionResult GetAllProduct(int id)
        {
            var result = _modelElectronicDetailService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("GetAllModelElectronicDetailDtoByModelId")]
        public IActionResult GetAllModelAccessoryDetailByModelId(int modelId)
        {
            var result = _modelElectronicDetailService.GetAllModelElectronicDetailDtoByModelId(modelId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetModelAccessoryDetailDtoById")]
        public IActionResult GetModelAccessoryDetailDtoById(int modelId)
        {
            var result = _modelElectronicDetailService.GetModelAccessoryDetailDtoById(modelId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(ModelElectronicDetail modelElectronicDetail)
        {
            var result = _modelElectronicDetailService.Add(modelElectronicDetail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(ModelElectronicDetail modelElectronicDetail)
        {
            var result = _modelElectronicDetailService.Update(modelElectronicDetail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("Delete")]
        public IActionResult Delete(ModelElectronicDetail modelElectronicDetail)
        {
            var result = _modelElectronicDetailService.Delete(modelElectronicDetail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
