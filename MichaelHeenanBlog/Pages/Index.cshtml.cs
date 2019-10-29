using System.Collections.Generic;
using System.Linq;
using JW;
using MichaelHeenanBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Sparc.TagCloud;

namespace MichaelHeenanBlog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BlogDbContext _dbContext;
        
        public List<BlogPostSummary> BlogPostSummarys { get; private set; }

        public List<TagCloudTag> TagCloud { get; private set; }

        public List<BlogPostSummary> PagedPosts { get; set; }
        
        public Pager Pager { get; set; }
        public int PageSize { get; set; }
        public int MaxPages { get; set; }

        public IndexModel(ILogger<IndexModel> logger, BlogDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            BlogPostSummarys = new List<BlogPostSummary>();
            
            // properties for pager parameter controls
            PageSize = 10;
            MaxPages = 10;
        }

        public IActionResult OnGet(int p = 1)
        {
            GetAllPostSummarys();

            // generate list of items to be paged
            var blogPostSummarys = BlogPostSummarys;

            // get pagination info for the current page
            Pager = new Pager(blogPostSummarys.Count(), p, PageSize, MaxPages);

            // assign the current page of items to the Items property
            PagedPosts = blogPostSummarys.Skip((Pager.CurrentPage - 1) * Pager.PageSize).Take(Pager.PageSize).ToList();

            TagCloud = GenerateTagCloud();

            return Page();
        }

        private void GetAllPostSummarys()
        {
            BlogPostSummarys = _dbContext
                .BlogPosts
                .OrderByDescending(b => b.CreatedAt)
                .Select(blogPost => new BlogPostSummary(blogPost.Id, blogPost.CreatedAt, blogPost.Title, blogPost.Body, blogPost.Tags)).ToList();
        }

        private List<TagCloudTag> GenerateTagCloud()
        {
            var analyzer = new TagCloudAnalyzer();

            var blogPostTags = _dbContext
                               .Tags
                               .Select(t => t.DisplayName)
                               .ToList();

            // blogPosts is an IEnumerable<String>, loaded from
            // the database.
            var tags = analyzer.ComputeTagCloud(blogPostTags);

            // Shuffle the tags, if you like for a random
            // display
            tags = tags.Shuffle();

            return tags.ToList();
        }
    }
}
