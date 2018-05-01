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

        public void SetDetails()
        {
            using (var db = new DatabaseContext())
            {

                foreach (KoloNaukowe group in Groups)
                {
                    GroupViewModels temp = new GroupViewModels();
                    Czlonkowie admin = new Czlonkowie();
                    admin = db.Czlonkowie.Include("Uzytkownik").First(p => p.IdKola == group.Id && p.Rola == 1);
                    temp.LeaderName = admin.Uzytkownik.Imie + " " + admin.Uzytkownik.Nazwisko;
                    temp.Mail = admin.Uzytkownik.Email;
                    temp.Group = group;
                    temp.PhoneNumber = "384782948";
                    viewGroup.Add(temp);
                }
            }
        }
    }
}