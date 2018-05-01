using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektIO.Models
{
    public class MembersViewModels
    {
        public List<Czlonkowie> Members { get; set; }
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
        public int GroupId { get; set; }
    }
}