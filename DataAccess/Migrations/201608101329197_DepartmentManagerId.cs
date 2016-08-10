namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartmentManagerId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Departments", name: "DepartmentManager_Id", newName: "DepartmentManagerId");
            RenameIndex(table: "dbo.Departments", name: "IX_DepartmentManager_Id", newName: "IX_DepartmentManagerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Departments", name: "IX_DepartmentManagerId", newName: "IX_DepartmentManager_Id");
            RenameColumn(table: "dbo.Departments", name: "DepartmentManagerId", newName: "DepartmentManager_Id");
        }
    }
}
