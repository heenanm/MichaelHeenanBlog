using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JW;
using MichaelHeenanBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Slugify;
using Sparc.TagCloud;

namespace MichaelHeenanBlog.Pages
{
    public class TagIndexModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public List<BlogPostSummary> BlogPosts { get; private set; }

        public string UrlFragment { get; set; }
        
        public List<BlogPostSummary> PagedPosts { get; private set; }

        public List<TagCloudTag> TagCloud { get; private set; }

        public List<TagInfo> TagGroupInfo { get; set; }

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

            //TagCloud = GenerateTagCloud();

            TagGroupInfo = GenerateTagInfo();

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


        //// Tag Cloud using sparc.tagcloud (lemmatisation) 
        //private List<TagCloudTag> GenerateTagCloud()
        //{
        //    var analyzer = new TagCloudAnalyzer();

        //    var blogPostTags = _blogDbContext
        //                       .Tags
        //                       .Select(t => t.DisplayName)
        //                       .ToList();

        //    // blogPosts is an IEnumerable<String>, loaded from
        //    // the database.
        //    var tags = analyzer.ComputeTagCloud(blogPostTags);

        //    // Shuffle the tags, if you like for a random
        //    // display
        //    tags = tags.Shuffle();

        //    return tags.ToList();
        //}

        // Tag Cloud and tag information generated without sparc.tagcloud
        private List<TagInfo> GenerateTagInfo()
        {
            var blogPostTags = _blogDbContext
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