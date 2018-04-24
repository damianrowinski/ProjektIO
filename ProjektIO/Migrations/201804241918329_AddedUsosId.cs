namespace ProjektIO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUsosId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uzytkownik", "UsosId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Uzytkownik", "UsosId");
        }
    }
}
