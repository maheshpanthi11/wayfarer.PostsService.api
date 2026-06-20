using wayfarer.PostsService.DataAccess.DataModels;

namespace wayfarer.PostsService.api.Service
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPostsByUserId(long userId);
    }
}
