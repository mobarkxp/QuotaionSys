namespace Quotaion.Client.Services
{
    public interface IRepositoryInterface<T> where T : class
    {
        Task<List<T>> GetAll(string ApiName);
        
        Task<T> GetById(string ApiId);
        Task<T> AddNew(T entity, string ApiName);
        Task DeleteById(string ApiId);
       Task Update(T entity, string ApiName);
    }
}
