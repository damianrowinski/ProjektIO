namespace ProjektIO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modeledmx : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KoloNaukowe", "SciezkaDoObrazu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.KoloNaukowe", "SciezkaDoObrazu");
        }
    }
}
