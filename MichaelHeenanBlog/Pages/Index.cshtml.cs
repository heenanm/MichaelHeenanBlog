using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MichaelHeenanBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MichaelHeenanBlog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BlogDbContext _dbContext;

        [BindProperty]
        public List<BlogPostSummary> BlogPostSummary { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, BlogDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult OnGet()
        {
            GetAllPostSummarys();

            return Page();
        }

        private void GetAllPostSummarys()
        {
            var blogPostsFiltered = new List<BlogPostSummary>();

            foreach (var blogPost in _dbContext.BlogPosts.Include(t => t.Tags))
            {
                var blogPostModel = new BlogPostSummary();
                blogPostModel.Title = blogPost.Title;
                blogPostModel.Body = blogPost.Body;
                blogPostModel.Tags = blogPost.Tags;              
                blogPostsFiltered.Add(blogPostModel);
            }

            BlogPostSummary = blogPostsFiltered;
        }
    }
}
