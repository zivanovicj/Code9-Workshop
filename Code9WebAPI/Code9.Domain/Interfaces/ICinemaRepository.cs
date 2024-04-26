using Code9.Domain.Models;

namespace Code9.Domain.Interfaces;

public interface ICinemaRepository
{
    public Task<List<Cinema>> GetAllCinema();

    public Task<Cinema> AddCinema(Cinema cinema);

    public Task<Cinema> UpdateCinema(Cinema cinema);

    public Task DeleteCinema(Cinema cinema);

    public Task<Cinema> GetCinema(Guid id);
}