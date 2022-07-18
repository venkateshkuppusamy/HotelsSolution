using HotelsAPI.Models;
using HotelsAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly HotelFilterValidator _validator;

        public HotelsController(IHotelService hotelService, HotelFilterValidator validator)
        {
            _hotelService = hotelService;
            _validator = validator;
        }

        // GET: api/<HotelsController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] HotelFilter? filter)
        {
            var validationResult = _validator.Validate(filter);
            
            if(validationResult.IsValid)
                return Ok(await _hotelService.Get(filter));
            return BadRequest(new ErrorResult (validationResult.Errors.Select(s => s.ErrorMessage).ToArray()));
        }
    }
}
