using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpSQlite.Model
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [InverseProperty("Author")]
        public List<Post> AuthoredPosts { get; set; }

        [InverseProperty("Contributor")]
        public List<Post> ContributedToPosts { get; set; }
    }
}
