using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MichaelHeenanBlog.Data
{
    public class AdminEntity
    {
        private readonly Guid _Id;
        private readonly string _Username;
        private readonly string _HashedPassword;

        public AdminEntity(string username, string password)
        {
            _Id = new Guid();
            _Username = username;
            _HashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
