namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeMail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Email", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Email");
        }
    }
}
