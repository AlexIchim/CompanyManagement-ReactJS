namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedTotalAllocation : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "TotalAllocation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "TotalAllocation", c => c.Int(nullable: false));
        }
    }
}
