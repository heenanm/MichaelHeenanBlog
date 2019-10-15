using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MichaelHeenanBlog.Data
{
    public class BlogPostSummary
    {
        public readonly string Title;
        public readonly string Body;
        public readonly IReadOnlyCollection<TagEntity> Tags;

        public BlogPostSummary(string title, string body, List<TagEntity> tags)
        {
            Title = title;
            Body = body;
            Tags = tags;
        } 
    }
}
