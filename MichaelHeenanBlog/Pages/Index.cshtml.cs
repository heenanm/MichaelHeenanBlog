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
        public List<BlogPostSummary> BlogPostSummarys { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, BlogDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            BlogPostSummarys = new List<BlogPostSummary>();
        }

        public IActionResult OnGet()
        {
            GetAllPostSummarys();

            return Page();
        }

        private void GetAllPostSummarys()
        {
            foreach (var blogPost in _dbContext.BlogPosts.Include(t => t.Tags))
            {
                var blogPostSummary = new BlogPostSummary(blogPost.Title, blogPost.Body, blogPost.Tags);
                BlogPostSummarys.Add(blogPostSummary);
            }
            
           
            //var BlogPostSummarys = from b in _dbContext.BlogPosts
            //                        let BlogPostSummary = new { b.Title, b.Body, b.Tags }
            //                        select BlogPostSummary;
        }
    }
}
