using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MichaelHeenanBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MichaelHeenanBlog.Pages
{
    public class CreateBlogPostModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public CreateBlogPostModel(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        [BindProperty]
        public BlogPost BlogPost { get; set; }

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

            _blogDbContext.BlogPosts.Add(BlogPost);
            //_blogDbContext.BlogPosts.Add(new BlogPost 
            //{
            //    Title = BlogPost.Title,
            //    Body = BlogPost.Body
            //});
            await _blogDbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
       
    }
}