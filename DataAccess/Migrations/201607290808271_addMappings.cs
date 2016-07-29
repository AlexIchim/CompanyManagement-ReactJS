namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMappings : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Departments", "Office_Id1", "dbo.Offices");
            DropIndex("dbo.Departments", new[] { "Office_Id" });
            DropIndex("dbo.Departments", new[] { "Office_Id1" });
            DropColumn("dbo.Departments", "Office_Id");
            RenameColumn(table: "dbo.Departments", name: "Office_Id1", newName: "Office_Id");
            AlterColumn("dbo.Departments", "Office_Id", c => c.Int());
            CreateIndex("dbo.Departments", "Office_Id");
            AddForeignKey("dbo.Departments", "Office_Id", "dbo.Offices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "Office_Id", "dbo.Offices");
            DropIndex("dbo.Departments", new[] { "Office_Id" });
            AlterColumn("dbo.Departments", "Office_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Departments", name: "Office_Id", newName: "Office_Id1");
            AddColumn("dbo.Departments", "Office_Id", c => c.Int());
            CreateIndex("dbo.Departments", "Office_Id1");
            CreateIndex("dbo.Departments", "Office_Id");
            AddForeignKey("dbo.Departments", "Office_Id1", "dbo.Offices", "Id", cascadeDelete: true);
        }
    }
}
