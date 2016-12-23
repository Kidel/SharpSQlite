using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpSQlite.Model
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public List<PostTag> PostTags { get; set; }
    }
}
