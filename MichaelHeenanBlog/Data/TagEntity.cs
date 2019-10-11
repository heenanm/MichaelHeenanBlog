using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MichaelHeenanBlog.Data
{
    public class TagEntity
    {
        public Guid Id { get; set; }
        public BlogPostEntity BlogPost { get; set; }
        public Guid BlogPostId { get; set; }
        public string UrlFragment { get; set; }
        public string DisplayName { get; set; }
    }
}
