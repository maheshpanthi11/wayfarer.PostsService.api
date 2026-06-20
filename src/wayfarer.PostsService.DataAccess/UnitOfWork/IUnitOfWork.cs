namespace wayfarer.PostsService.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get; }
        Task<int> CompleteAsync();
    }
}
