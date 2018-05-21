using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektIO.Libraries
{
    public class UrlLibrary
    {
        public string GroupFirst = "Group";
        public List<string> GroupSecond = new List<string> { "ShowMembers", "ShowPosts", "ChangeImage"};
        public string PostFirst = "Post";
        public List<string> PostSecond = new List<string> { "EditPost", "DeletePost", "EditComment", "DeleteComment" };
        public string LeaderFirst = "Leader";
        public List<string> LeaderSecond = new List<string> { "EditStatute" };

    }
}