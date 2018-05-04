using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektIO.Models
{
    public class PostListViewModels
    {
        public List<Post> Posts { get; set; }
        public List<string> AuthorsNames { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}