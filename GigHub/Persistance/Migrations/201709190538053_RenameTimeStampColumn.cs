namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameTimeStampColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gigs", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Gigs", "TimeStamp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Gigs", "TimeStamp", c => c.DateTime(nullable: false));
            DropColumn("dbo.Gigs", "DateTime");
        }
    }
}
