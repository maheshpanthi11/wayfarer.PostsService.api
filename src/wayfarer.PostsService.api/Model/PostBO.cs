using System.ComponentModel.DataAnnotations;

namespace wayfarer.PostsService.api.Model
{
    public class PostBO
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string PostType { get; set; }
    }
}
