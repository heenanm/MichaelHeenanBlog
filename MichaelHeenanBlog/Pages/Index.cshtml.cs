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
        public List<BlogPostModel> BlogPostModels { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, BlogDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult OnGet()
        {
            GetAllPosts();

            return Page();
        }

        private void GetAllPosts()
        {
            var blogPostsFiltered = new List<BlogPostModel>();

            foreach (var blogPost in _dbContext.BlogPosts)
            {
                var blogPostModel = new BlogPostModel();
                blogPostModel.Title = blogPost.Title;
                blogPostModel.Body = blogPost.Body;
                blogPostModel.Tags = blogPost.Tags;
                blogPostsFiltered.Add(blogPostModel);
            }

            BlogPostModels = blogPostsFiltered;
        }

        public class BlogPostModel
        {
            public string Title { get; set; }
            public string Body { get; set; }
            public List<TagEntity> Tags { get; set; }
        }
    }
}
