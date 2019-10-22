using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MichaelHeenanBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Slugify;

namespace MichaelHeenanBlog.Areas.Admin.Pages
{
    public class EditPostModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public EditPostResponse PostContent { get; private set; }

        [BindProperty]
        public EditPostRequest NewPostContent { get; set; }

        [BindProperty]
        public Guid BlogPostId { get; set; }

        public EditPostModel(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public IActionResult OnGet(Guid blogPostId)
        {
            GetBlogPost(blogPostId);

            return Page();
        }

        public IActionResult OnPost()
        {
            EditBlogPost(BlogPostId);

            return RedirectToPage("/Index", new { area = "admin" });
        }

        public void GetBlogPost(Guid blogPostId)
        {
           var BlogPost = _blogDbContext
               .BlogPosts
               .Select(b => new BlogPostEntity
               {
                   Id = b.Id,
                   Title = b.Title,
                   Body = b.Body,
                   CreatedAt = b.CreatedAt,
                   Tags = b.Tags,
                   Comments = b.Comments
               })
               .SingleOrDefault(b => b.Id == blogPostId);

            BlogPostId = blogPostId;

            var tagList = new StringBuilder();

            foreach (var tag in BlogPost.Tags)
            {
                tagList.Append(tag.DisplayName + ",");
            }

            PostContent = new EditPostResponse(BlogPost.Title, BlogPost.Body, tagList.ToString());
            
        }

        public void EditBlogPost(Guid Id)
        {
            var postToEdit = _blogDbContext.BlogPosts
                .Include(b => b.Tags)
                .SingleOrDefault(b => b.Id == Id);

            postToEdit.CreatedAt = DateTime.UtcNow;
            postToEdit.Title = NewPostContent.Title;
            postToEdit.Body = NewPostContent.Body;

            var tagList = new string[] { };
            if (!string.IsNullOrEmpty(NewPostContent.Tags))
            {
                tagList = NewPostContent.Tags.Split(",");
            }

            var tagEntities = new List<TagEntity>();

            // Initialise slugify helper
            var helper = new SlugHelper();

            foreach (var tag in tagList)
            {
                if (!string.IsNullOrEmpty(tag))
                {
                    var urlSlug = helper.GenerateSlug(tag);

                    var tagEntity = new TagEntity
                    {
                        BlogPostId = Id,
                        DisplayName = tag,
                        UrlFragment = urlSlug
                    };
                    tagEntities.Add(tagEntity);
                }
            }

            postToEdit.Tags = tagEntities;

            _blogDbContext.SaveChanges();
        }

        public class EditPostRequest
        {
            public string Title { get; set; }
            public string Body { get; set; }
            public string Tags { get; set; }
        }

        public class EditPostResponse
        {
            public EditPostResponse(string title, string body, string tags)
            {
                Title = title;
                Body = body;
                Tags = tags;
            }

            public string Title { get; }
            public string Body { get; }
            public string Tags { get; }
        }
    }
}
