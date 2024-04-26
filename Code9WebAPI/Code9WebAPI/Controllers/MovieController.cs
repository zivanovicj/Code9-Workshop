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
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddMovieRequest> _validator;

        public MovieController(IMediator mediator, IValidator<AddMovieRequest> validationRules)
        {
            _mediator = mediator;
            _validator = validationRules;
        }

        [HttpGet]
        public async Task<ActionResult<List<Movie>>> GetAllMovies()
        {
            var query = new GetAllMoviesQuery();
            var movie = await _mediator.Send(query);
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> AddMovie(AddMovieRequest addMovieRequest)
        {
            var validationResult = _validator.Validate(addMovieRequest);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var addMovieCommand = new AddMovieCommand
            {
                Title = addMovieRequest.Title,
                ReleaseYear = addMovieRequest.ReleaseYear,
                Rating = addMovieRequest.Rating
            };

            var Movie = await _mediator.Send(addMovieCommand);
            return CreatedAtAction(nameof(GetAllMovies), new { id = Movie.Id }, Movie);
        }
    }
}
