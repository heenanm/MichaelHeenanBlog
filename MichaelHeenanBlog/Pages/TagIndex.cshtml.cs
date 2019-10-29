using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JW;
using MichaelHeenanBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sparc.TagCloud;

namespace MichaelHeenanBlog.Pages
{
    public class TagIndexModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public List<BlogPostSummary> BlogPosts { get; private set; }

        public string UrlFragment { get; set; }
        [BindProperty]
        public List<BlogPostSummary> PagedPosts { get; private set; }

        [BindProperty]
        public List<TagCloudTag> TagCloud { get; private set; }

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

            TagCloud = GenerateTagCloud();

            return Page();
        }

        private void GetBlogPosts(string urlFragment)
        {
            UrlFragment = urlFragment;

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

        private List<TagCloudTag> GenerateTagCloud()
        {
            var analyzer = new TagCloudAnalyzer();

            var blogPostTags = _blogDbContext
                               .BlogPosts
                               .SelectMany
                               (
                                    b => b.Tags.Select
                                    (
                                        t => t.DisplayName
                                    )
                                ).ToList();

            // blogPosts is an IEnumerable<String>, loaded from
            // the database.
            var tags = analyzer.ComputeTagCloud(blogPostTags);

            // Shuffle the tags, if you like for a random
            // display
            tags = tags.Shuffle();

            var tagCloudData = new TagCloudData();

            tagCloudData.TagCloud = tags.ToList();

            return tagCloudData.TagCloud;
        }
    }
}