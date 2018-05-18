using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektIO.Libraries
{
    public class UrlLibrary
    {
        public string GroupFirst = "Group";
        public List<string> GroupSecond = new List<string> { "ShowMembers", "ShowPosts"};
        public string PostFirst = "Post";
        public List<string> PostSecond = new List<string> { "EditPost", "DeletePost", "EditComment", "DeleteComment" };
    }
}