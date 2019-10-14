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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    var entityTypeConfigurations = Assembly.GetExecutingAssembly()
        //                                           .GetTypes()
        //                                           .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition &&
        //                                                       t.GetTypeInfo().ImplementedInterfaces.Any(i => i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

        //    foreach (var entityTypeConfiguration in entityTypeConfigurations)
        //    {
        //        dynamic config = Activator.CreateInstance(entityTypeConfiguration);
        //        modelBuilder.ApplyConfiguration(config);
        //    }
        //}
    }
}
