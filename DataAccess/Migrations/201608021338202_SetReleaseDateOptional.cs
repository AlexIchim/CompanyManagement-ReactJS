namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetReleaseDateOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "ReleaseDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "ReleaseDate", c => c.DateTime(nullable: false));
        }
    }
}
