using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JW;
using Markdig;
using MichaelHeenanBlog.Data;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace MichaelHeenanBlog.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BlogDbContext _dbContext;

        public List<BlogPostSummary> BlogPostSummarys { get; private set; }

        [BindProperty]
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

            // generate list of sample items to be paged
            var blogPostSummarys = BlogPostSummarys;

            // get pagination info for the current page
            Pager = new Pager(blogPostSummarys.Count(), p, PageSize, MaxPages);

            // assign the current page of items to the Items property
            PagedPosts = blogPostSummarys.Skip((Pager.CurrentPage - 1) * Pager.PageSize).Take(Pager.PageSize).ToList();

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
                .OrderByDescending(b => b.CreatedAt)
                .Select(blogPost => new BlogPostSummary(blogPost.Id, blogPost.CreatedAt, blogPost.Title, blogPost.Body, blogPost.Tags)).ToList();
        }
    }
}
