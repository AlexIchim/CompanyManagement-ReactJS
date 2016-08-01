namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateModelTest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        EmploymentDate = c.DateTime(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        TotalAllocation = c.Int(nullable: false),
                        JobType = c.Int(nullable: false),
                        PositionType = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.EmployeeProjects",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        Allocation = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeId, t.ProjectId })
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.String(),
                        Duration = c.Int(),
                        DepartmentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Offices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Departments", "OfficeId", c => c.Int(nullable: false));
            AddColumn("dbo.Departments", "DepartmentManager_Id", c => c.Int());
            CreateIndex("dbo.Departments", "OfficeId");
            CreateIndex("dbo.Departments", "DepartmentManager_Id");
            AddForeignKey("dbo.Departments", "DepartmentManager_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.Departments", "OfficeId", "dbo.Offices", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "OfficeId", "dbo.Offices");
            DropForeignKey("dbo.Departments", "DepartmentManager_Id", "dbo.Employees");
            DropForeignKey("dbo.EmployeeProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.EmployeeProjects", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Projects", new[] { "DepartmentId" });
            DropIndex("dbo.EmployeeProjects", new[] { "ProjectId" });
            DropIndex("dbo.EmployeeProjects", new[] { "EmployeeId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Departments", new[] { "DepartmentManager_Id" });
            DropIndex("dbo.Departments", new[] { "OfficeId" });
            DropColumn("dbo.Departments", "DepartmentManager_Id");
            DropColumn("dbo.Departments", "OfficeId");
            DropTable("dbo.Offices");
            DropTable("dbo.Projects");
            DropTable("dbo.EmployeeProjects");
            DropTable("dbo.Employees");
        }
    }
}
