namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customized : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.short_urls", "customized", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.short_urls", "customized");
        }
    }
}
