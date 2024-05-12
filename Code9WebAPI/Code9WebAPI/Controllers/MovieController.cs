using Code9.Domain.Commands;
using Code9.Domain.Models;
using Code9.Domain.Queries;
using Code9WebAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Code9WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
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
