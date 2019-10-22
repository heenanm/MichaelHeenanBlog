using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MichaelHeenanBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MichaelHeenanBlog.Areas.Admin.Pages
{
    public class DeletePostModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;
        
        public BlogPostEntity BlogPost { get; set; }

        [BindProperty]
        public Guid BlogPostId { get; set; }

        public DeletePostModel(BlogDbContext blogDbContext)
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
            DeleteBlogPost(BlogPostId);

            return RedirectToPage("/Index", new { area = "admin" });
        }

        public void GetBlogPost(Guid blogPostId)
        {
            BlogPost = _blogDbContext
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
        }

        public void DeleteBlogPost(Guid Id)
        {
            var postToDelete =_blogDbContext
            .BlogPosts
            .SingleOrDefault(b => b.Id == Id);

            _blogDbContext.Remove(postToDelete);
            _blogDbContext.SaveChanges();

        }
    }
}
