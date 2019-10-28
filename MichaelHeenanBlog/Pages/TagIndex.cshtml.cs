using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JW;
using MichaelHeenanBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MichaelHeenanBlog.Pages
{
    public class TagIndexModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public List<BlogPostSummary> BlogPosts { get; private set; }

        [BindProperty]
        public List<BlogPostSummary> PagedPosts { get; set; }

        public Pager Pager { get; set; }
        public int PageSize { get; set; }
        public int MaxPages { get; set; }

        public TagIndexModel(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
            BlogPosts = new List<BlogPostSummary>();

            // properties for pager parameter controls
            PageSize = 10;
            MaxPages = 10;
        }

        public IActionResult OnGet(string urlFragment, int p = 1)
        {
            GetBlogPosts(urlFragment);

            // generate list of items to be paged
            var blogPostSummarys = BlogPosts;

            // get pagination info for the current page
            Pager = new Pager(blogPostSummarys.Count(), p, PageSize, MaxPages);

            // assign the current page of items to the Items property
            PagedPosts = blogPostSummarys.Skip((Pager.CurrentPage - 1) * Pager.PageSize).Take(Pager.PageSize).ToList();

            return Page();
        }

        private void GetBlogPosts(string urlFragment)
        {
            var blogPostIds = _blogDbContext.Tags
                            .Where(t => t.UrlFragment == urlFragment)
                            .Select(t => t.BlogPostId).ToList();

            foreach (var id in blogPostIds)
            {
                var blogPost = _blogDbContext.BlogPosts
                               .Where(b => b.Id == id)
                               .Select(blogPost => new BlogPostSummary(blogPost.Id, blogPost.CreatedAt, blogPost.Title, blogPost.Body, blogPost.Tags)).ToList();
            
                    BlogPosts.AddRange(blogPost);
            }

            BlogPosts = BlogPosts
                        .OrderByDescending(b => b.DateCreated).ToList();
        }
    }
}