using Code9.Domain.Interfaces;
using Code9.Domain.Models;
using Code9.Domain.Queries;
using MediatR;

namespace Code9.Domain.Handlers
{
    public class GetAllMoviesHandler : IRequestHandler<GetAllMoviesQuery, List<Movie>>
    {
        private readonly IMovieRepository _movieRepository;

        public GetAllMoviesHandler(
            IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<Movie>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            return await _movieRepository.GetAllMovies();
        }
    }
}
