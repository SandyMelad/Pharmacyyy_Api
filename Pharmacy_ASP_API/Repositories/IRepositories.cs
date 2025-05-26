namespace Pharmacy_ASP_API.Repositories
{
    public interface IRepositories<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id); 
        Task AddAsync(T entity);
        Task UpdateAsync(T entity, string id);  
        Task DeleteAsync(string id);  
    }

}
