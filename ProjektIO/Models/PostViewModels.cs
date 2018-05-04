using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektIO.Models
{
    public class PostViewModels
    {
        public Post Post { get; set; }
        public List<Komentarz> Comments { get; set; }
        public List<string> CommentsAuthors { get; set; }
        public string AuthorName { get; set; }
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
    }
}