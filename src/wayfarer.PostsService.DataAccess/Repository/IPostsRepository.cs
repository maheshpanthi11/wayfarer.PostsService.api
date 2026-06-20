using System;
using System.Linq.Expressions;
using wayfarer.PostsService.DataAccess.DataModels;

namespace wayfarer.PostsService.DataAccess
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<IEnumerable<Post>> GetByExpression(Expression<Func<Post, bool>> predicate);
    }
}
