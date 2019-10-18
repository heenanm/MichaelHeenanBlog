namespace MichaelHeenanBlog.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using MichaelHeenanBlog.Data;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using BCrypt.Net;

    public class SignInModel : PageModel
    {
        private readonly BlogDbContext _blogDbContext;

        [BindProperty]
        public SignInModelInput AdminLogin { get; set; }


        public SignInModel(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public async Task<IActionResult> OnPost()
        {
            var user = _blogDbContext.Administrators
                                     .FirstOrDefault(u => u.Username == AdminLogin.Username);


            if (user == null || !BCrypt.Verify(AdminLogin.Password, user.HashedPassword))
            {
                return RedirectToPage("./Index");
            }
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, AdminLogin.Username),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToPage("/Index", new { area="admin"});
        }

        public class SignInModelInput
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}