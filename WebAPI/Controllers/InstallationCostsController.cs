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
    public class InstallationCostsController : ControllerBase
    {
        IInstallationCostService _installationCostService;
        public InstallationCostsController(IInstallationCostService installationCostService)
        {
            _installationCostService = installationCostService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllInstallationCost()
        {
            var result = _installationCostService.GetAllInstallationCost();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _installationCostService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetAllInstallationCostDto")]
        public IActionResult GetAllInstallationCostDto()
        {
            var result = _installationCostService.GetAllInstallationCostDto();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetInstallationCostByLocationId")]
        public IActionResult GetInstallationCostByLocationId(int locationId)
        {
            var result = _installationCostService.GetInstallationCostByLocationId(locationId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("Add")]
        public IActionResult Add(InstallationCost installationCost)
        {
            var result = _installationCostService.Add(installationCost);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(InstallationCost installationCost)
        {
            var result = _installationCostService.Update(installationCost);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(InstallationCost installationCost)
        {
            var result = _installationCostService.Delete(installationCost);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
