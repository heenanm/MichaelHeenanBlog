using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MichaelHeenanBlog.Data
{
    public class BlogDbContext : DbContext
    {
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }
    }
}
