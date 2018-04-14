namespace ProjektIO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Czlonkowie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdKola = c.Int(nullable: false),
                        Rola = c.Int(nullable: false),
                        Aktywny = c.Boolean(nullable: false),
                        IdUzytkownika = c.Int(nullable: false),
                        KoloNaukowe_Id = c.Int(),
                        Uzytkownik_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KoloNaukowe", t => t.KoloNaukowe_Id)
                .ForeignKey("dbo.Uzytkownik", t => t.Uzytkownik_Id)
                .Index(t => t.KoloNaukowe_Id)
                .Index(t => t.Uzytkownik_Id);
            
            CreateTable(
                "dbo.KoloNaukowe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        DataUtworzenia = c.DateTime(nullable: false),
                        Uczelnia = c.Int(nullable: false),
                        Aktywny = c.Boolean(nullable: false),
                        DataDoUsuniecia = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Portfolio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdKola = c.Int(nullable: false),
                        Zawartosc = c.String(),
                        DataUtworzenia = c.DateTime(nullable: false),
                        KoloNaukowe_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KoloNaukowe", t => t.KoloNaukowe_Id)
                .Index(t => t.KoloNaukowe_Id);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdUzytkownika = c.Int(nullable: false),
                        IdKola = c.Int(nullable: false),
                        Zawartosc = c.String(),
                        DataUtworzenia = c.DateTime(nullable: false),
                        Przypiety = c.Boolean(nullable: false),
                        KoloNaukowe_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KoloNaukowe", t => t.KoloNaukowe_Id)
                .Index(t => t.KoloNaukowe_Id);
            
            CreateTable(
                "dbo.Komentarz",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdPostu = c.Int(nullable: false),
                        Zawartosc = c.String(),
                        DataPrzeslania = c.DateTime(nullable: false),
                        IdUzytkownika = c.Int(nullable: false),
                        Post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Post", t => t.Post_Id)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.Uzytkownik",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Imie = c.String(),
                        Nazwisko = c.String(),
                        DataUtworzenia = c.DateTime(nullable: false),
                        DataModyfikacji = c.DateTime(nullable: false),
                        DataLogowania = c.DateTime(nullable: false),
                        Login = c.String(),
                        Email = c.String(),
                        Haslo = c.String(),
                        IDP = c.Int(nullable: false),
                        Salt = c.String(),
                        Rola = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Uczestnictwo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdUzytkownika = c.Int(nullable: false),
                        IdEventu = c.Int(nullable: false),
                        Event_Id = c.Int(),
                        Uzytkownik_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Event", t => t.Event_Id)
                .ForeignKey("dbo.Uzytkownik", t => t.Uzytkownik_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Uzytkownik_Id);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        Lokalizacja = c.String(),
                        DataUtworzenia = c.DateTime(nullable: false),
                        DataRozpoczecia = c.DateTime(nullable: false),
                        DataZakonczenia = c.DateTime(nullable: false),
                        Zawartosc = c.String(),
                        IdKola = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Wiadomosc",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOdbiorcy = c.Int(nullable: false),
                        IdNadawcy = c.Int(nullable: false),
                        Zawartosc = c.String(),
                        Tytul = c.String(),
                        DataPrzeslania = c.DateTime(nullable: false),
                        DataOdebrania = c.DateTime(nullable: false),
                        Nadawca_Id = c.Int(),
                        Odbiorca_Id = c.Int(),
                        Uzytkownik_Id = c.Int(),
                        Uzytkownik_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Uzytkownik", t => t.Nadawca_Id)
                .ForeignKey("dbo.Uzytkownik", t => t.Odbiorca_Id)
                .ForeignKey("dbo.Uzytkownik", t => t.Uzytkownik_Id)
                .ForeignKey("dbo.Uzytkownik", t => t.Uzytkownik_Id1)
                .Index(t => t.Nadawca_Id)
                .Index(t => t.Odbiorca_Id)
                .Index(t => t.Uzytkownik_Id)
                .Index(t => t.Uzytkownik_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wiadomosc", "Uzytkownik_Id1", "dbo.Uzytkownik");
            DropForeignKey("dbo.Wiadomosc", "Uzytkownik_Id", "dbo.Uzytkownik");
            DropForeignKey("dbo.Wiadomosc", "Odbiorca_Id", "dbo.Uzytkownik");
            DropForeignKey("dbo.Wiadomosc", "Nadawca_Id", "dbo.Uzytkownik");
            DropForeignKey("dbo.Uczestnictwo", "Uzytkownik_Id", "dbo.Uzytkownik");
            DropForeignKey("dbo.Uczestnictwo", "Event_Id", "dbo.Event");
            DropForeignKey("dbo.Czlonkowie", "Uzytkownik_Id", "dbo.Uzytkownik");
            DropForeignKey("dbo.Komentarz", "Post_Id", "dbo.Post");
            DropForeignKey("dbo.Post", "KoloNaukowe_Id", "dbo.KoloNaukowe");
            DropForeignKey("dbo.Portfolio", "KoloNaukowe_Id", "dbo.KoloNaukowe");
            DropForeignKey("dbo.Czlonkowie", "KoloNaukowe_Id", "dbo.KoloNaukowe");
            DropIndex("dbo.Wiadomosc", new[] { "Uzytkownik_Id1" });
            DropIndex("dbo.Wiadomosc", new[] { "Uzytkownik_Id" });
            DropIndex("dbo.Wiadomosc", new[] { "Odbiorca_Id" });
            DropIndex("dbo.Wiadomosc", new[] { "Nadawca_Id" });
            DropIndex("dbo.Uczestnictwo", new[] { "Uzytkownik_Id" });
            DropIndex("dbo.Uczestnictwo", new[] { "Event_Id" });
            DropIndex("dbo.Komentarz", new[] { "Post_Id" });
            DropIndex("dbo.Post", new[] { "KoloNaukowe_Id" });
            DropIndex("dbo.Portfolio", new[] { "KoloNaukowe_Id" });
            DropIndex("dbo.Czlonkowie", new[] { "Uzytkownik_Id" });
            DropIndex("dbo.Czlonkowie", new[] { "KoloNaukowe_Id" });
            DropTable("dbo.Wiadomosc");
            DropTable("dbo.Event");
            DropTable("dbo.Uczestnictwo");
            DropTable("dbo.Uzytkownik");
            DropTable("dbo.Komentarz");
            DropTable("dbo.Post");
            DropTable("dbo.Portfolio");
            DropTable("dbo.KoloNaukowe");
            DropTable("dbo.Czlonkowie");
        }
    }
}
