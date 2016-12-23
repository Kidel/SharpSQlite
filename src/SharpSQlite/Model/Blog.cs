using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpSQlite.Model
{
    public class Blog
    {
        public int BlogId { get; set; }
        [Required]
        public string Name { get; set; }

        public List<Post> Posts { get; set; }
        public BlogMetadata Metadata { get; set; }
    }

    [NotMapped]
    public class BlogMetadata
    {
        public DateTime LoadedFromDatabase { get; set; }
    }
}
