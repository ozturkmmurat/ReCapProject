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
    public class CarsController : ControllerBase
    {
        ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("GetAllCars")]
        public IActionResult GetAllCars()
        {
            var result = _carService.GetAllCars();

            if (result.Success)
            {
               return  Ok(result);
            }
            return BadRequest(result);

        }
        [HttpGet("GetCarsByBrandId")]
        public IActionResult GetCarsByBrandId(int id)
        {
            var result = _carService.GetCarsByBrandId(id);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetCarsByColorId")]
        public IActionResult GetCarsByColorId(int id)
        {
            var result = _carService.GetCarsByColorId(id);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetCarsDetailDTO")]
        public IActionResult GetCarsDetailDTO()
        {
            var result = _carService.GetCarsDetailDTO();

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetByBrandNameByColorNameCarsDetailDTO")]
        public IActionResult GetByBrandNameByColorNameCarsDetailDTO(string brandName, string colorName)
        {
            var result = _carService.GetByBrandNameByColorNameCarDetails(brandName, colorName);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetCarsDetailByBrandId")]
        public IActionResult GetCarsDetailByBrandId(int brandId)
        {
            var result = _carService.GetCarDetailsByBrandId(brandId);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetCarsDetailByColorId")]
        public IActionResult GetCarsDetailByColorId(int colorId)
        {
            var result = _carService.GetCarDetailsByColorId(colorId);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetCarsIdDetailDTO")]
        public IActionResult GetCarsIdDetailDTO(int id)
        {
            var result = _carService.GetCarsIdDetailDTO(id);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _carService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Add")]
        public IActionResult Add(Car car)
        {
            var result = _carService.Add(car);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Update")]
        public IActionResult Update(Car car)
        {
            var result = _carService.Update(car);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Delete")]
        public IActionResult Delete(Car car)
        {
            var result = _carService.Delete(car);

            if (result.Success)
            {
                Ok(result);
            }
            return BadRequest(result);
        }
    }
}