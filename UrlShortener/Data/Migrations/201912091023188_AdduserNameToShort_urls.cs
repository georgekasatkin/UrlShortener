namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdduserNameToShort_urls : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.short_urls", "UserName", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.short_urls", "UserName");
        }
    }
}
