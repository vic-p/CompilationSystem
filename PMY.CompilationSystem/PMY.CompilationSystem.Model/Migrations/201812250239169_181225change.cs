namespace PMY.CompilationSystem.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _181225change : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskLists", "CMDCommand", c => c.String(nullable: false));
            AddColumn("dbo.TaskLists", "TaskType", c => c.Int(nullable: false));
            AddColumn("dbo.TaskLists", "TaskStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.TaskLists", "ActionPath", c => c.String(maxLength: 100));
            DropColumn("dbo.TaskLists", "ActionType");
            DropColumn("dbo.TaskLists", "State");
            DropColumn("dbo.TaskLists", "CreatorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskLists", "CreatorId", c => c.Int(nullable: false));
            AddColumn("dbo.TaskLists", "State", c => c.Int(nullable: false));
            AddColumn("dbo.TaskLists", "ActionType", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.TaskLists", "ActionPath", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.TaskLists", "TaskStatus");
            DropColumn("dbo.TaskLists", "TaskType");
            DropColumn("dbo.TaskLists", "CMDCommand");
        }
    }
}
