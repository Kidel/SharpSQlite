namespace SharpSQlite.Model
{
    public class Movie
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string UrlSlug { get; set; }

        public Category Category { get; set; }
    }
}
