using Markdig;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MichaelHeenanBlog.Data
{
    public class BlogPostSummary
    {
        public readonly Guid BlogPostId;
        public readonly DateTime DateCreated;
        public readonly string Title;
        public readonly HtmlString Body;
        public readonly IReadOnlyCollection<TagEntity> Tags;
        public readonly int TimeToRead;

        public BlogPostSummary(Guid blogPostId, DateTime dateCreated, string title, string body, List<TagEntity> tags)
        {
            var pipeline = new MarkdownPipelineBuilder().UseEmphasisExtras().Build();
            BlogPostId = blogPostId;
            DateCreated = dateCreated;
            Title = title;
            Body = new HtmlString(Markdown.ToHtml(body, pipeline));
            Tags = tags;
            var wordCount = body.Split().Where(x => !string.IsNullOrEmpty(x)).Count();
            TimeToRead = (int)Math.Ceiling((double)wordCount / (double)250);
        }
    }
}
