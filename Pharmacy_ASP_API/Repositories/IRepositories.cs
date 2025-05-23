namespace Pharmacy_ASP_API.Repositories
{
    public interface IRepositories<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id); 
        Task AddAsync(T entity);
        Task UpdateAsync(T entity, Guid id);  
        Task DeleteAsync(Guid id);  
    }

}
