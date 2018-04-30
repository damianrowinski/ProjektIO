using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjektIO;

namespace ProjektIO.Models
{
    public class GroupsViewModel
    {
        public List<KoloNaukowe> Groups { get; set; }
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
        public int Category { get; set; }
    }
}