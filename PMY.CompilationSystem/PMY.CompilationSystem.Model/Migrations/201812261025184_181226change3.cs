namespace PMY.CompilationSystem.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _181226change3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaskLists", "CMDCommand", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaskLists", "CMDCommand", c => c.String(nullable: false));
        }
    }
}
