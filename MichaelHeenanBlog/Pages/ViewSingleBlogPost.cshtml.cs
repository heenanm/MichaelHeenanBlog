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
    public class ViewSingleBlogPostModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public ViewSingleBlogPostModel(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        [BindProperty]
        public CommentInput Comment { get; set; }
        [BindProperty]
        public Guid BlogPostId { get; set; }

        public List<CommentEntity> Comments { get; private set; }

        public BlogPostSummary BlogPost { get; private set; }

        public IActionResult OnGet(Guid blogPostId)
        {
            GetBlogPost(blogPostId);

            return Page();
        }

        public IActionResult OnPost()
        {
            PostComment(BlogPostId);

            return RedirectToPage("/ViewSingleBlogPost", BlogPostId);

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

        public void PostComment(Guid Id)
        {
            var postToEdit = _blogDbContext.BlogPosts
               .Include(b => b.Comments)
               .SingleOrDefault(b => b.Id == Id);

            if (!string.IsNullOrEmpty(Comment.Author) && !string.IsNullOrEmpty(Comment.Comment))
            { 
                var commentEntity = new CommentEntity { 
                BlogPostId = postToEdit.Id,
                AuthorName = Comment.Author,
                CreatedAt = DateTime.UtcNow,
                Body = Comment.Comment
                };

                postToEdit.Comments.Add(commentEntity);
            }

            _blogDbContext.SaveChanges();
        }

        public class CommentInput
        {
            public string Author { get; set; }
            public string Comment { get; set; }
        }
    }
}