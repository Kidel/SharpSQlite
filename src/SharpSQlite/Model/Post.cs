﻿using System.Collections.Generic;

namespace SharpSQlite.Model
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }

        public int? BlogId { get; set; }
        public Blog Blog { get; set; }

        public User Author { get; set; }

        public User Contributor { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}
