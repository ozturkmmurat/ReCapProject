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
    public class CreditCartController : ControllerBase
    {
        ICreditCartService _creditCartService;

        public CreditCartController(ICreditCartService creditCartService)
        {
            _creditCartService = creditCartService;
        }

        [HttpPost("Add")]
        public IActionResult Add(CreditCard creditCard)
        {
            var result = _creditCartService.Add(creditCard);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(CreditCard creditCard)
        {
            var result = _creditCartService.Delete(creditCard);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _creditCartService.GetAllCreditCard();

            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _creditCartService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetByUserId")]
        public IActionResult GetByUserId(int id)
        {
            var result = _creditCartService.GetByUserId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
