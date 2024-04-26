using Code9.Domain.Commands;
using Code9.Domain.Interfaces;
using MediatR;

namespace Code9.Domain.Handlers
{
    public class DeleteCinemaHandler : IRequestHandler<DeleteCinemaCommand, Unit>
    {
        private readonly ICinemaRepository _cinemaRepository;

        public DeleteCinemaHandler(
            ICinemaRepository cinemaRepository)
        {
            _cinemaRepository = cinemaRepository;
        }

        public async Task<Unit> Handle(DeleteCinemaCommand request, CancellationToken cancellationToken)
        {
            var cinemaEntity = await _cinemaRepository.GetCinema(request.Id);

            if (cinemaEntity is null)
                throw new Exception($"Cinema with Id: {request.Id} was not found in database.");

            await _cinemaRepository.DeleteCinema(cinemaEntity);

            return Unit.Value;
        }
    }
}
