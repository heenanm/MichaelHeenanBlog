using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MichaelHeenanBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MichaelHeenanBlog.Pages
{
    public class TagIndexModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public TagIndexModel(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public void OnGet(Guid tagId)
        {
            
        }

        //private void GetBlogPosts(Guid tagId)
        //{
        //    var blogPostsByTag = _blogDbContext
        //        .BlogPosts
        //        .Where(b => b.Tags)



        //}
    }
}