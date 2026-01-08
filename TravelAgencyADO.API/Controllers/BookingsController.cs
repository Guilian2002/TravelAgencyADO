using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TravelAgencyADO.API.DTOs;
using TravelAgencyADO.BLL.Services.Interfaces;

namespace TravelAgencyADO.API.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _service;
        public BookingsController(IBookingService service) => _service = service;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var booking = _service.GetById(id);
            return booking is null ? NotFound() : Ok(booking);
        }

        [HttpPost]
        public IActionResult Create([FromBody] BookingCreateDTO dto)
        {
            var created = _service.Create(dto.bookingDate, dto.clientName, dto.activityIds);

            return CreatedAtAction(nameof(GetById), new { id = created!.Id }, created);
        }

        //[HttpPut("{id:guid}")]
        //public IActionResult Update(Guid id, [FromBody] TodoUpdateDto dto)
        //{
        //    var userId = GetUserId();

        //    var renamed = _service.Rename(userId, id, dto.Title);
        //    if (!renamed) return NotFound();

        //    if (dto.IsDone)
        //    {
        //        var done = _service.MarkAsDone(userId, id);
        //        if (!done) return NotFound();
        //    }

        //    return NoContent();
        //}

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var deleted = _service.Delete(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
