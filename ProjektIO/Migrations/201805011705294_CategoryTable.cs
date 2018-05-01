namespace ProjektIO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kategoria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        Skrot = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.KoloNaukowe", "Regulamin", c => c.String());
            AddColumn("dbo.KoloNaukowe", "KategoriaId", c => c.Int(nullable: false));
            AddColumn("dbo.Uzytkownik", "NumerTelefonu", c => c.String());
            CreateIndex("dbo.KoloNaukowe", "KategoriaId");
            AddForeignKey("dbo.KoloNaukowe", "KategoriaId", "dbo.Kategoria", "Id", cascadeDelete: true);
            //DropColumn("dbo.KoloNaukowe", "Kategoria");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KoloNaukowe", "Kategoria", c => c.Int(nullable: false));
            DropForeignKey("dbo.KoloNaukowe", "KategoriaId", "dbo.Kategoria");
            DropIndex("dbo.KoloNaukowe", new[] { "KategoriaId" });
            DropColumn("dbo.Uzytkownik", "NumerTelefonu");
            DropColumn("dbo.KoloNaukowe", "KategoriaId");
            DropColumn("dbo.KoloNaukowe", "Regulamin");
            DropTable("dbo.Kategoria");
        }
    }
}
