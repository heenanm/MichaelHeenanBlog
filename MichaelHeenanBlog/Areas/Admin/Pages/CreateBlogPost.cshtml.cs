using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MichaelHeenanBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Slugify;

namespace MichaelHeenanBlog.Areas.Admin.Pages
{
    public class CreateBlogPostModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public CreateBlogPostModel(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        [BindProperty]
        public BlogPostModelInput BlogPost { get; set; }
        
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var blogPost = new BlogPostEntity
            {
                CreatedAt = DateTime.UtcNow,
                Title = BlogPost.Title,
                Body = BlogPost.Body,
                Comments = new List<CommentEntity>()
            };

            var tagList = new string [] {};
            if (!string.IsNullOrEmpty(BlogPost.Tags))
            {
               tagList = BlogPost.Tags.Split(",");
            }

            var tagEntities = new List<TagEntity>();

            // Initialise slugify helper
            var helper = new SlugHelper();

            foreach (var tag in tagList)
            {
                var urlSlug = helper.GenerateSlug(tag);

                var tagEntity = new TagEntity
                {
                    BlogPostId = blogPost.Id,
                    DisplayName = tag,
                    UrlFragment = urlSlug 
            };
                tagEntities.Add(tagEntity);
            }
            
            blogPost.Tags = tagEntities;
            
            _blogDbContext.BlogPosts.Add(blogPost);
           
            await _blogDbContext.SaveChangesAsync();

            return RedirectToPage("/Index", new { area = "admin" });
        }

        public class BlogPostModelInput
        {
            [Required]
            public string Title { get; set; }
            [Required]
            public string Body { get; set; }
            public string Tags { get; set; }
        }
       
    }
}