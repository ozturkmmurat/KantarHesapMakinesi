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
    public class SizeContentsController : ControllerBase
    {
        ISizeContentService _sizeContent;
        public SizeContentsController(ISizeContentService sizeContent)
        {
            _sizeContent = sizeContent;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllHeightWeight()
        {
            var result = _sizeContent.GetAllSizeContent();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetAllSizeCtDtoBySizeId")]
        public IActionResult GetAllSizeCtDtoBySizeId(int sizeId)
        {
            var result = _sizeContent.GetAllSizeCtDtoBySizeId(sizeId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _sizeContent.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(SizeContent sizeContent)
        {
            var result = _sizeContent.Add(sizeContent);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(SizeContent sizeContent)
        {
            var result = _sizeContent.Update(sizeContent);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(SizeContent sizeContent)
        {
            var result = _sizeContent.Delete(sizeContent);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
