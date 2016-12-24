using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharpSQlite.Model
{
    public class Post
    {
        public int PostId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        [Required]
        public string Slug { get; set; }

        public int? BlogId { get; set; }
        public Blog Blog { get; set; }

        public int? AuthorId { get; set; }
        public User Author { get; set; }

        public int? ContributorId { get; set; }
        public User Contributor { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}
