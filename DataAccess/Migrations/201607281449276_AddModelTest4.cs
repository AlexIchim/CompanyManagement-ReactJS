namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelTest4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "Department_Id", "dbo.Departments");
            AddColumn("dbo.Departments", "DepartmentManager_Id", c => c.Int());
            AddColumn("dbo.Employees", "Department_Id1", c => c.Int());
            CreateIndex("dbo.Departments", "DepartmentManager_Id");
            CreateIndex("dbo.Employees", "Department_Id1");
            AddForeignKey("dbo.Departments", "DepartmentManager_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.Employees", "Department_Id1", "dbo.Departments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Department_Id1", "dbo.Departments");
            DropForeignKey("dbo.Departments", "DepartmentManager_Id", "dbo.Employees");
            DropIndex("dbo.Employees", new[] { "Department_Id1" });
            DropIndex("dbo.Departments", new[] { "DepartmentManager_Id" });
            DropColumn("dbo.Employees", "Department_Id1");
            DropColumn("dbo.Departments", "DepartmentManager_Id");
            AddForeignKey("dbo.Employees", "Department_Id", "dbo.Departments", "Id");
        }
    }
}
