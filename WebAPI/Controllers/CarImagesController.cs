using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        ICarImagesService _carImagesService;
        public CarImagesController(ICarImagesService carImagesService)
        {
            _carImagesService = carImagesService;
        }
        [HttpGet("GetCarsById")]
        public IActionResult GetCarsById(int id)
        {
            var result = _carImagesService.GetCarsById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        public IActionResult GetById(int id)
        {
            var result = _carImagesService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Add")]
        public IActionResult Add([FromForm] IFormFile file ,[FromForm]CarImages carImages)
        {
            var result = _carImagesService.Add(file, carImages);
            if (result.Success)
            {
              return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Update")]
        public IActionResult Update([FromForm] IFormFile file , [FromForm] CarImages carImages)
        {
            var result = _carImagesService.Update(file, carImages);
            if (result.Success)
            {
                Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Delete")]
        public IActionResult Delete(CarImages carImages)
        {
            var result = _carImagesService.Delete(carImages);
            if (result.Success)
            {
                Ok(result);
            }
            return BadRequest(result);
        }
    }
}