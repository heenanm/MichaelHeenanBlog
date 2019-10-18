using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MichaelHeenanBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MichaelHeenanBlog.Pages
{
    public class ViewSingleBlogPostModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public ViewSingleBlogPostModel(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public BlogPostEntity BlogPost { get; set; }

        public IActionResult OnGet(Guid blogPostId)
        {
            GetBlogPost(blogPostId);

            return Page();
        }

        public void GetBlogPost(Guid blogPostId)
        {
            BlogPost = _blogDbContext
               .BlogPosts
               .Select(b => new BlogPostEntity {
                Id = b.Id, 
                Title = b.Title,
                Body = b.Body, 
                CreatedAt = b.CreatedAt,
                Tags = b.Tags,
                Comments = b.Comments
                })
               .SingleOrDefault(b => b.Id == blogPostId);
        
        }
    }
}