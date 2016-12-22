using System.Collections.Generic;

namespace SharpSQlite.Model
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UrlSlug { get; set; }

        public List<Movie> Movies { get; set; }

    }
}
