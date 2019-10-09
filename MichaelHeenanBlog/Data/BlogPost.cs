using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MichaelHeenanBlog.Data
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
