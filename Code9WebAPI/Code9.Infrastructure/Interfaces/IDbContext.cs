namespace Code9.Infrastructure.Interfaces
{
    public interface IDbContext
    {
        Task<T> AddEntity<T>(T entity);
        Task RemoveEntity<T>(T entity);
        Task<T> UpdateEntity<T>(T entity);
        Task DeleteEntity<T>(T entity);
        Task<T> GetEntity<T>(Guid id);
        Task<T> GetEntities<T>();
    }
}
