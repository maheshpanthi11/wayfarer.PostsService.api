namespace wayfarer.PostsService.DataAccess
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(long id);
        Task AddAsync(T entity);
        void Update(T entity); // EF tracks changes, update doesn't need to be async
        void Delete(T entity);
    }
}
