using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyADO.API.DTOs;
using TravelAgencyADO.BLL.Services.Interfaces;
using TravelAgencyADO.Domain.Entities;

namespace TravelAgencyADO.API.Controllers
{
    [ApiController]
    [Route("api/activities")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService _service;
        public ActivitiesController(IActivityService service) => _service = service;

        [HttpGet]
        public IActionResult GetAll(Guid destinationId)
        {
            return Ok(_service.GetAll(destinationId));
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var activity = _service.GetById(id);
            return activity is null ? NotFound() : Ok(activity);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ActivityCreateDTO dto)
        {
            var created = _service.Create(dto.title, dto.description, dto.price, dto.destinationId);

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
