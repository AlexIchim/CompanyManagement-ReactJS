namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Departments", "OfficeId", "dbo.Offices");
            DropIndex("dbo.Projects", new[] { "DepartmentId" });
            DropIndex("dbo.Departments", new[] { "OfficeId" });
            RenameColumn(table: "dbo.Projects", name: "DepartmentId", newName: "Department_Id");
            RenameColumn(table: "dbo.Departments", name: "OfficeId", newName: "Office_Id");
            AddColumn("dbo.Employees", "Department_Id1", c => c.Int());
            AlterColumn("dbo.Projects", "Department_Id", c => c.Int());
            AlterColumn("dbo.Departments", "Office_Id", c => c.Int());
            CreateIndex("dbo.Employees", "Department_Id1");
            CreateIndex("dbo.Departments", "Office_Id");
            CreateIndex("dbo.Projects", "Department_Id");
            AddForeignKey("dbo.Employees", "Department_Id1", "dbo.Departments", "Id");
            AddForeignKey("dbo.Projects", "Department_Id", "dbo.Departments", "Id");
            AddForeignKey("dbo.Departments", "Office_Id", "dbo.Offices", "Id");
            DropColumn("dbo.Employees", "DepartmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "DepartmentId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Departments", "Office_Id", "dbo.Offices");
            DropForeignKey("dbo.Projects", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.Employees", "Department_Id1", "dbo.Departments");
            DropIndex("dbo.Projects", new[] { "Department_Id" });
            DropIndex("dbo.Departments", new[] { "Office_Id" });
            DropIndex("dbo.Employees", new[] { "Department_Id1" });
            AlterColumn("dbo.Departments", "Office_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Projects", "Department_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Employees", "Department_Id1");
            RenameColumn(table: "dbo.Departments", name: "Office_Id", newName: "OfficeId");
            RenameColumn(table: "dbo.Projects", name: "Department_Id", newName: "DepartmentId");
            CreateIndex("dbo.Departments", "OfficeId");
            CreateIndex("dbo.Projects", "DepartmentId");
            AddForeignKey("dbo.Departments", "OfficeId", "dbo.Offices", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
    }
}
