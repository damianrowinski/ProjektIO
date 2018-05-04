using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektIO.Models
{
    public class AddCommentViewModels
    {
        public Komentarz Comment { get; set; }
        public string CommentContent { get; set; }
        public int PostId { get; set; }
    }
}