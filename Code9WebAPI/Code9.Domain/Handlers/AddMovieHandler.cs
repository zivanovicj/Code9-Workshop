using Code9.Domain.Commands;
using Code9.Domain.Interfaces;
using Code9.Domain.Models;
using MediatR;

namespace Code9.Domain.Handlers
{
    public class AddMovieHandler : IRequestHandler<AddMovieCommand, Movie>
    {
        private readonly IMovieRepository _movieRepository;

        public AddMovieHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Movie> Handle(AddMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = new Movie
            {
                Title = request.Title,
                ReleaseYear = request.ReleaseYear,
                Rating = request.Rating
            };

            return await _movieRepository.AddMovie(movie);
        }
    }
}
