using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MichaelHeenanBlog.Data
{
    public class BlogDbContext : DbContext
    {
        public DbSet<BlogPostEntity> BlogPosts { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }
    }
}
