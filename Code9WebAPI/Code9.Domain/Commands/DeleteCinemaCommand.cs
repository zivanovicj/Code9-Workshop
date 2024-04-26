using MediatR;

namespace Code9.Domain.Commands
{
    public record DeleteCinemaCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
