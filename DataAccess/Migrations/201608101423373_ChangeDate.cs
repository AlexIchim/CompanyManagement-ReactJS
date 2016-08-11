namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "EmploymentDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "EmploymentDate", c => c.DateTime(nullable: false));
        }
    }
}
