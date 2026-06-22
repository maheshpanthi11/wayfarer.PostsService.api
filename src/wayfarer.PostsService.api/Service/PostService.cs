using wayfarer.PostsService.api.Model;
using wayfarer.PostsService.DataAccess;
using wayfarer.PostsService.DataAccess.DataModels;

namespace wayfarer.PostsService.api.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;

        public PostService(IPostRepository repository)
        {
            this._repository = repository;
        }

        public async Task<IEnumerable<Post>> GetPostsByUserId(long userId)
        {
            return await _repository.GetByExpression(p => p.UserId == userId);
        }

        public async Task<Post> SavePost(PostBO postBo)
        {
            try
            {
                var post = new Post
                {
                    UserId = postBo.UserId,
                    Url = postBo.Url,
                    Timestamp = DateTime.UtcNow,
                    PostType = postBo.PostType
                };
                var result = await _repository.AddAsync(post);
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
