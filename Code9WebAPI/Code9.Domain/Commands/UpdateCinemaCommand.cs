using Code9.Domain.Models;
using MediatR;

namespace Code9.Domain.Commands
{
    public record UpdateCinemaCommand : IRequest<Cinema>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public string Street { get; set; }
        public int NumberOfAuditoriums { get; set; }
    }
}
