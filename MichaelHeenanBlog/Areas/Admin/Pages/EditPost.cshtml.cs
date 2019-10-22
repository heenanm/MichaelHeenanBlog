using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MichaelHeenanBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MichaelHeenanBlog.Areas.Admin.Pages
{
    public class EditPostModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        public BlogPostEntity BlogPost { get; set; }

        [BindProperty]
        public EditedPostContent PostContent { get; set; }

        [BindProperty]
        public Guid BlogPostId { get; set; }

        public EditPostModel(BlogDbContext blogDbContext)
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
            EditBlogPost(BlogPostId);

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

        public void EditBlogPost(Guid Id)
        {
            var postToEdit = _blogDbContext
            .BlogPosts
            .SingleOrDefault(b => b.Id == Id);

            postToEdit.CreatedAt = DateTime.UtcNow;
            postToEdit.Title = PostContent.Title;
            postToEdit.Body = PostContent.Body;
            postToEdit.Tags = PostContent.Tags;
            postToEdit.Comments = PostContent.Comments;
         
            _blogDbContext.SaveChanges();
        }

        public class EditedPostContent
        {
            public string Title { get; set; }
            public string Body { get; set; }
            public List<TagEntity> Tags { get; set; }
            public List<CommentEntity> Comments { get; set; }
        }
    }
}
