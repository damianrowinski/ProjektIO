using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektIO.Models
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public List<Komentarz> Comments { get; set; }
        public List<string> CommentsAuthors { get; set; }
        public string AuthorName { get; set; }
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
    }

    public class PostListViewModel
    {
        public List<Post> Posts { get; set; }
        public KoloNaukowe Group { get; set; }
        public List<string> AuthorsNames { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }

    public class AddCommentViewModel
    {
        public Komentarz Comment { get; set; }
        public string CommentContent { get; set; }
        public int PostId { get; set; }
    }

    public class AddPostViewModel
    {
        public Czlonkowie Member { get; set; }
        public string PostContent { get; set; }
    }
}