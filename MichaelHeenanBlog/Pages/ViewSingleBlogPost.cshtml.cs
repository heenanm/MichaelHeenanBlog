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

        public IEnumerable<BlogPostEntity> BlogPost { get; set; }

        public IActionResult OnGet(Guid blogPostId)
        {
            GetBlogPost(blogPostId);

            return Page();
        }

        public void GetBlogPost(Guid blogPostId)
        {
            BlogPost = _blogDbContext
                .BlogPosts
                .Where(b => b.Id == blogPostId)
                .ToList();
        }
    }
}