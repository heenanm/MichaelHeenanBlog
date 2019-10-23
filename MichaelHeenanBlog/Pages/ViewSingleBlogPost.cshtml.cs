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

        public List<CommentEntity> Comments { get; private set; }

        public BlogPostSummary BlogPost { get; private set; }

        public IActionResult OnGet(Guid blogPostId)
        {
            GetBlogPost(blogPostId);

            return Page();
        }

        public void GetBlogPost(Guid blogPostId)
        {
            var blogPost = _blogDbContext
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

            BlogPost = new BlogPostSummary(blogPost.Id, blogPost.CreatedAt, blogPost.Title, blogPost.Body, blogPost.Tags);
            Comments = blogPost.Comments.ToList();       
        }
    }
}