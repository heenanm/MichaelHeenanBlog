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
            //var posts = _dbContext.BlogPosts.Select(blogPost => new BlogPostSummary(blogPost.Title, blogPost.Body, blogPost.Tags));
            //var enumerator = posts.GetEnumerator();

            //while (enumerator.MoveNext()){
            //    var post = enumerator.Current;
            //}

            //foreach (var post in posts)
            //{
            //    BlogPostSummarys.Add(post);
            //}

            BlogPostSummarys = _dbContext
                .BlogPosts
                .Select(blogPost => new BlogPostSummary(blogPost.Title, blogPost.Body, blogPost.Tags)).ToList();
        }
    }
}
