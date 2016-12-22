using System.ComponentModel.DataAnnotations;

namespace SharpSQlite.Model
{
    public class Movie
    {
        public int MovieId { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string UrlSlug { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
