namespace ProjektIO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentBlock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Post", "AktywneKom", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Post", "AktywneKom");
        }
    }
}
