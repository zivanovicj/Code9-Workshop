using Code9.Domain.Commands;
using Code9.Domain.Interfaces;
using Code9.Domain.Models;
using MediatR;

namespace Code9.Domain.Handlers
{
    public class UpdateCinemaHandler : IRequestHandler<UpdateCinemaCommand, Cinema>
    {
        private readonly ICinemaRepository _cinemaRepository;

        public UpdateCinemaHandler(
            ICinemaRepository cinemaRepository)
        {
            _cinemaRepository = cinemaRepository;
        }

        public async Task<Cinema> Handle(UpdateCinemaCommand request, CancellationToken cancellationToken)
        {
            var cinemaEntity = await _cinemaRepository.GetCinema(request.Id);

            if (cinemaEntity is null)
                throw new Exception($"Cinema with Id: {request.Id} was not found in database.");

            cinemaEntity.Name = request.Name;
            cinemaEntity.CityId = request.CityId;
            cinemaEntity.NumberOfAuditoriums = request.NumberOfAuditoriums;

            return await _cinemaRepository.UpdateCinema(cinemaEntity);
        }
    }
}
