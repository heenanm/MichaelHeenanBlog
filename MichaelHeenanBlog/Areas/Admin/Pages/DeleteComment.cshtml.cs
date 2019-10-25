using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MichaelHeenanBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MichaelHeenanBlog.Areas.Admin.Pages
{
    public class DeleteCommentModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public Comment CommentData { get; private set; }
        
        [BindProperty]
        public Guid CommentId { get;  set; }

        public Guid CommentBlogPostId {get; private set;}

        public DeleteCommentModel(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public IActionResult OnGet(Guid commentId)
        {
            GetComment(commentId);

            return Page();
        }

        public IActionResult OnPost()
        {
            DeleteComment(CommentId);

            return RedirectToPage("/ViewSingleBlogPost", new { blogpostId = CommentBlogPostId }) ;
        }

        public void GetComment(Guid Id)
        {
            var blogComment = _blogDbContext
              .Comments
              .Select(c => new CommentEntity
              {
                  Id = c.Id,
                  BlogPostId = c.BlogPostId,
                  AuthorName = c.AuthorName,
                  Body = c.Body,
                  CreatedAt = c.CreatedAt,
              })
              .SingleOrDefault(c => c.Id == Id);

            CommentData = new Comment
            {
                Author = blogComment.AuthorName,
                Body = blogComment.Body,
                Date = blogComment.CreatedAt
            };

            CommentBlogPostId = blogComment.BlogPostId;
            CommentId = Id;
        }

        public void DeleteComment(Guid Id)
        {
            var blogComment = _blogDbContext
            .Comments
            .Select(c => new CommentEntity
            {
                Id = c.Id,
                BlogPostId = c.BlogPostId,
                AuthorName = c.AuthorName,
                Body = c.Body,
                CreatedAt = c.CreatedAt,
            })
            .SingleOrDefault(c => c.Id == Id);

            CommentBlogPostId = blogComment.BlogPostId;

            _blogDbContext.Remove(blogComment);
            _blogDbContext.SaveChanges();
        }

        public class Comment
        {
            public Guid Id { get; set; }
            public DateTime Date { get; set; }
            public string Author { get; set; }
            public string Body { get; set; }
        }
    }
}
