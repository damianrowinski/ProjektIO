using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjektIO;

namespace ProjektIO.Models
{
    public class GroupsListViewModels
    {
        public List<KoloNaukowe> Groups { get; set; }
        public List<GroupViewModels> viewGroup = new List<GroupViewModels>();
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
        public string Category { get; set; }
    }
}