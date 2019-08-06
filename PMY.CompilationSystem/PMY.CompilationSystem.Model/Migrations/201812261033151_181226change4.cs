namespace PMY.CompilationSystem.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _181226change4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaskLists", "FinishTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaskLists", "FinishTime", c => c.DateTime(nullable: false));
        }
    }
}
