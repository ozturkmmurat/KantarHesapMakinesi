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
    public class ModelMaterialDetailsController : ControllerBase
    {
        IModelMaterialDetailService _modelDetailService;
        public ModelMaterialDetailsController(IModelMaterialDetailService modelDetailService)
        {
            _modelDetailService = modelDetailService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _modelDetailService.GetAllModelDetail();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _modelDetailService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("Add")]
        public IActionResult Add(ModelMaterialDetail modelDetail)
        {
            var result = _modelDetailService.Add(modelDetail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(ModelMaterialDetail modelDetail)
        {
            var result = _modelDetailService.Update(modelDetail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(ModelMaterialDetail modelDetail)
        {
            var result = _modelDetailService.Delete(modelDetail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
