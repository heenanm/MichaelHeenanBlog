using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MichaelHeenanBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
                Body = BlogPost.Body
            };

            var tagList = BlogPost.Tags.Split(" ").Where(x => !string.IsNullOrEmpty(x));
            var tagEntities = new List<TagEntity>();
            
            foreach (var tag in tagList)
            {
                var tagEntity = new TagEntity
                {
                    BlogPostId = blogPost.Id,
                    DisplayName = tag,
                    UrlFragment = System.Web.HttpUtility.UrlEncode(tag)
            };
                tagEntities.Add(tagEntity);
            }
            
            blogPost.Tags = tagEntities;
            
            _blogDbContext.BlogPosts.Add(blogPost);
           
            await _blogDbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public class BlogPostModelInput 
        {
            public string Title { get; set; }
            public string Body { get; set; }
            public string Tags { get; set; }
        }
       
    }
}