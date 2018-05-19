using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektIO.Models
{
    public class GroupViewModel
    {
        public KoloNaukowe Group { get; set; }
        public string LeaderName { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
    }

    public class GroupListViewModel
    {
        public List<KoloNaukowe> Groups { get; set; }
        public List<GroupViewModel> viewGroup = new List<GroupViewModel>();
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
        public string Category { get; set; }
        public string CategoryName { get; set; }
    }

    public class MembersViewModel
    {
        public List<Czlonkowie> Members { get; set; }
        public KoloNaukowe Group { get; set; }
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
    }

    public class AddGroupViewModel
    {
        public KoloNaukowe Group { get; set; }
        public string Category { get; set; }
    }
}