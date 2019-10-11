using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MichaelHeenanBlog.Data
{
    public class CommentEntity
    {
        public Guid Id { get; set; }
        public BlogPostEntity BlogPost { get; set; }
        public Guid BlogPostId { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AuthorName { get; set; }
    }
}
