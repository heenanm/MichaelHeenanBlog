using System;
using System.Collections.Generic;
using System.Linq;
using JW;
using MichaelHeenanBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Slugify;
using Sparc.TagCloud;

namespace MichaelHeenanBlog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BlogDbContext _dbContext;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public List<BlogPostSummary> BlogPostSummarys { get; private set; }

        public List<TagCloudTag> TagCloud { get; private set; }

        public List<TagInfo> TagGroupInfo { get; set; }

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
            if (string.IsNullOrEmpty(SearchString))
            {
                GetAllPostSummarys();
            }
            else
            {
                GetFilteredPosts(SearchString);
            }

            // generate list of items to be paged
            var blogPostSummarys = BlogPostSummarys;

            // get pagination info for the current page
            Pager = new Pager(blogPostSummarys.Count(), p, PageSize, MaxPages);

            // assign the current page of items to the Items property
            PagedPosts = blogPostSummarys.Skip((Pager.CurrentPage - 1) * Pager.PageSize).Take(Pager.PageSize).ToList();

            //TagCloud = GenerateTagCloud();

            TagGroupInfo = GenerateTagInfo();

            return Page();
        }

        public IActionResult OnPost()
        {
            return OnGet(1);
        }

        private void GetAllPostSummarys()
        {
            BlogPostSummarys = _dbContext
                .BlogPosts
                .OrderByDescending(b => b.CreatedAt)
                .Select(blogPost => new BlogPostSummary(blogPost.Id, blogPost.CreatedAt, blogPost.Title, blogPost.Body, blogPost.Tags)).ToList();
        }

        private void GetFilteredPosts(string searchString)
        {
            BlogPostSummarys = _dbContext
               .BlogPosts
               .Where(b => b.Title.Contains(searchString) || b.Body.Contains(searchString))
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

        // Tag Cloud and tag information generated without sparc.tagcloud
        private List<TagInfo> GenerateTagInfo()
        {
            var blogPostTags = _dbContext
                              .Tags
                              .Select(t => t.DisplayName)
                              .ToList();

            var groupTags = blogPostTags.GroupBy(t => t)
                                        .OrderBy(t => t.Count());

            var maxTagCount = groupTags.Last().Count();

            var randomGroup = groupTags.OrderBy(a => Guid.NewGuid());

            var tagInfoList = new List<TagInfo>();

            foreach (var tag in randomGroup)
            {
                var helper = new SlugHelper();
                var tagInfo = new TagInfo()
                {
                    DisplayName = tag.Key,
                    TagCount = tag.Count(),
                    Category = GenerateCategory(tag.Count(), maxTagCount),
                    UrlSlug = helper.GenerateSlug(tag.Key)
                };

                tagInfoList.Add(tagInfo);
            }

            return tagInfoList;
        }

        public class TagInfo
        {
            public string DisplayName { get; set; }
            public int TagCount { get; set; }
            public int Category { get; set; }
            public string UrlSlug { get; set; }
        }

        private int GenerateCategory(int tagCount, int maxTagCount)
        {
            var category = 10 - ((double)tagCount / (double)maxTagCount) * 10;

            return (int)category;

            //var rnd = new Random();
            //return rnd.Next(10);
        }
    }
}
