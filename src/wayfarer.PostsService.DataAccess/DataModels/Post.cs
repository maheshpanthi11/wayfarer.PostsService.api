using System.ComponentModel.DataAnnotations;

namespace wayfarer.PostsService.DataAccess.DataModels
{
    public class Post
    {
        [Key]
        public long Id { get; set; }

        public long UserId { get; set; }

        public string Url { get; set; }

        public DateTime Timestamp { get; set; }

        public string PostType { get; set; }
    }
}
