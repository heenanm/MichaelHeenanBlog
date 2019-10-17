using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MichaelHeenanBlog.Data
{
    public class AdminSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BlogDbContext(serviceProvider.GetRequiredService<DbContextOptions<BlogDbContext>>()))
            {
                if (!context.Administrators.Any())
                {
                    context.Administrators.AddRange
                    (
                        new AdminEntity()
                        {
                            Id = Guid.NewGuid(),
                            Username = "Admin1",
                            HashedPassword = BCrypt.Net.BCrypt.HashPassword("Admin123")
                        },

                        new AdminEntity()
                        {
                            Id = Guid.NewGuid(),
                            Username = "Admin2",
                            HashedPassword = BCrypt.Net.BCrypt.HashPassword("AdminABC")
                        }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}
