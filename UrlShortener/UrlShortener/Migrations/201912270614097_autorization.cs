namespace UrlShortener.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class autorization : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "PasswordHash", c => c.String());
            AlterColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String());
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AlterColumn("dbo.AspNetUserClaims", "ClaimType", c => c.String());
            AlterColumn("dbo.AspNetUserClaims", "ClaimValue", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUserClaims", "ClaimValue", c => c.String(unicode: false));
            AlterColumn("dbo.AspNetUserClaims", "ClaimType", c => c.String(unicode: false));
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String(unicode: false));
            AlterColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String(unicode: false));
            AlterColumn("dbo.AspNetUsers", "PasswordHash", c => c.String(unicode: false));
        }
    }
}
