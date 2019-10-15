using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MichaelHeenanBlog.Data
{
    public class BlogPostSummary
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public List<TagEntity> Tags { get; set; }
    }
}
