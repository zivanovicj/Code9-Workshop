using Code9.Domain.Models;
using MediatR;

namespace Code9.Domain.Queries
{
    public record GetAllMoviesQuery : IRequest<List<Movie>>;
}
