using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using wayfarer.PostsService.DataAccess.DataModels;

namespace wayfarer.PostsService.DataAccess
{
    public class PostsRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetByExpression( Expression<Func<Post, bool>> predicate)
        {
            // Enforces query encapsulation away from the controller
            return await _context.Set<Post>()
                .Where(predicate)
                .ToListAsync();
        }
    }
}
