using Code9.Domain.Commands;
using Code9.Domain.Models;
using Code9.Domain.Queries;
using Code9WebAPI.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Code9WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddCinemaRequest> _validator;

        public CinemaController(IMediator mediator, IValidator<AddCinemaRequest> validationRules)
        {
            _mediator = mediator;
            _validator = validationRules;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cinema>>> GetAllCinema()
        {
            var query = new GetAllCinemaQuery();
            var cinema = await _mediator.Send(query);
            return Ok(cinema);
        }

        [HttpPost]
        public async Task<ActionResult<Cinema>> AddCinema(AddCinemaRequest addCinemaRequest)
        {
            var validationResult = _validator.Validate(addCinemaRequest);
            
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var addCinemaCommand = new AddCinemaCommand
            {
                Name = addCinemaRequest.Name,
                CityId = addCinemaRequest.CityId,
                NumberOfAuditoriums = addCinemaRequest.NumberOfAuditoriums
            };

            var cinema = await _mediator.Send(addCinemaCommand);
            return CreatedAtAction(nameof(GetAllCinema), new { id = cinema.Id }, cinema);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Cinema>> UpdateCinema([FromRoute]Guid id, UpdateCinemaRequest updateCinemaRequest)
        {
            var updateCommand = new UpdateCinemaCommand
            {
                Id = id,
                Name = updateCinemaRequest.Name,
                CityId = updateCinemaRequest.CityId,
                NumberOfAuditoriums = updateCinemaRequest.NumberOfAuditoriums
            };

            var cinema = await _mediator.Send(updateCommand);
            
            if (cinema is null)
            {
                return NotFound();
            }
            return Ok(cinema);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCinema([FromRoute]Guid id)
        {
            var command = new DeleteCinemaCommand { Id = id };
            
            await _mediator.Send(command);
            
            return NoContent();
        }
    }
}
