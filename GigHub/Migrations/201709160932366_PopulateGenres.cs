namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenres : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Genres (Id, Name) VALUES(1, 'Jazz')");
            Sql("Insert into Genres (Id, Name) VALUES(2, 'Blues')");
            Sql("Insert into Genres (Id, Name) VALUES(3, 'Rock')");
            Sql("Insert into Genres (Id, Name) VALUES(4, 'Country')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Genres WHERE Id IN(1, 2, 3, 4)");
        }
    }
}
