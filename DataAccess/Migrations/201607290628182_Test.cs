namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Departments", "OfficeId", "dbo.Offices");
            DropForeignKey("dbo.Employees", "Department_Id", "dbo.Departments");
            DropIndex("dbo.Departments", new[] { "OfficeId" });
            DropIndex("dbo.Employees", new[] { "Department_Id1" });
            DropColumn("dbo.Employees", "Department_Id");
            RenameColumn(table: "dbo.Employees", name: "Department_Id1", newName: "Department_Id");
            AddColumn("dbo.Employees", "DepartmentId", c => c.Int(nullable: false));
            AlterColumn("dbo.Departments", "OfficeId", c => c.Int());
            CreateIndex("dbo.Departments", "OfficeId");
            CreateIndex("dbo.Employees", "DepartmentId");
            AddForeignKey("dbo.Departments", "OfficeId", "dbo.Offices", "Id");
            AddForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Departments", "OfficeId", "dbo.Offices");
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Departments", new[] { "OfficeId" });
            AlterColumn("dbo.Departments", "OfficeId", c => c.Int(nullable: false));
            DropColumn("dbo.Employees", "DepartmentId");
            RenameColumn(table: "dbo.Employees", name: "Department_Id", newName: "Department_Id1");
            AddColumn("dbo.Employees", "Department_Id", c => c.Int());
            CreateIndex("dbo.Employees", "Department_Id1");
            CreateIndex("dbo.Departments", "OfficeId");
            AddForeignKey("dbo.Employees", "Department_Id", "dbo.Departments", "Id");
            AddForeignKey("dbo.Departments", "OfficeId", "dbo.Offices", "Id", cascadeDelete: true);
        }
    }
}
