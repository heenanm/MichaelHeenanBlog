using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MichaelHeenanBlog.Data
{
    public class BlogPostSummary
    {
        public readonly Guid BlogPostId;
        public readonly string Title;
        public readonly string Body;
        public readonly IReadOnlyCollection<TagEntity> Tags;

        public BlogPostSummary(Guid blogPostId, string title, string body, List<TagEntity> tags)
        {
            BlogPostId = blogPostId;
            Title = title;
            Body = body;
            Tags = tags;
        } 
    }
}
