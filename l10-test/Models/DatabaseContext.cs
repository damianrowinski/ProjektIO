using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace l10_test.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DefaultConnection")
        {

        }

        public DbSet<Czlonkowie> Czlonkowie { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<KoloNaukowe> KoloNaukowe { get; set; }
        public DbSet<Komentarz> Komentarz { get; set; }
        public DbSet<Portfolio> Portfolio { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Uczestnictwo> Uczestnictwo { get; set; }
        public DbSet<Uzytkownik> Uzytkownik { get; set; }
        public DbSet<Wiadomosc> Wiadomosc { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}