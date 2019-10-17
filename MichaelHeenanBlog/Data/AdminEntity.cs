using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MichaelHeenanBlog.Data
{
    public class AdminEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
    }
}
