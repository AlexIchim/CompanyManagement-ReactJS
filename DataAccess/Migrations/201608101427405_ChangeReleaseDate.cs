namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeReleaseDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "ReleaseDate", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "ReleaseDate", c => c.DateTime());
        }
    }
}
