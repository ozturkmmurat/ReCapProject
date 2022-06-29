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
        public IActionResult Add(CreditCard creditCart,int amount)
        {
            var result = _creditCartService.Payment(creditCart,amount);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
