using wayfarer.PostsService.api.Model;
using wayfarer.PostsService.DataAccess.DataModels;

namespace wayfarer.PostsService.api.Service
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPostsByUserId(long userId);

        Task<Post> SavePost(PostBO postBo);
    }
}
