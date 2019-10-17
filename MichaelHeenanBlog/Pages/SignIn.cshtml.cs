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
    public class SignInModel : PageModel
    {
        private readonly DbContext _dbContext;

        [BindProperty]
        public SignInModelInput AdminLogin { get; set; }


        public SignInModel(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public void OnGet()
        {
            
        }



        public class SignInModelInput
        {
            public Guid Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}