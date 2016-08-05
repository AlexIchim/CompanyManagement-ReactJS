namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedProjectModelStatusFromStringToEnum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "Status", c => c.String());
        }
    }
}
