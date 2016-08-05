namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "EmploymentDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Employees", "ReleaseDate", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "ReleaseDate", c => c.DateTime());
            AlterColumn("dbo.Employees", "EmploymentDate", c => c.DateTime(nullable: false));
        }
    }
}
