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
    public class ModelsController : ControllerBase 
    {
        IModelService _modelService;
        public ModelsController(IModelService modelService)
        {
            _modelService = modelService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllModel()
        {
            var result = _modelService.GetAllModel();

            if (result.Success)
            { 
                return Ok(result);
            }
             
            return BadRequest(result);
        }

        [HttpGet("GetAllByProductId")]
        public IActionResult GetAllByProductId(int productId)
        {
            var result = _modelService.GetAllByProductId(productId);

            if (result.Success)
            { 
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetAllDto")]
        public IActionResult GetAllProductDto()
        {
            var result = _modelService.GetAllModelDto();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _modelService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("AddDto")]
        public IActionResult AddDto(ModelDto modelDto)
        {
            var result = _modelService.AddDto(modelDto);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("UpdateDto")]
        public IActionResult UpdateDto(ModelDto modelDto)
        {
            var result = _modelService.UpdateDto(modelDto);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(Model model)
        {
            var result = _modelService.Update(model);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(Model model)
        {
            var result = _modelService.Delete(model);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
