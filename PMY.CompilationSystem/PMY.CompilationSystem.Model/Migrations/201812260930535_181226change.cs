namespace PMY.CompilationSystem.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _181226change : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "ProjectStartItem", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "ProjectStartItem");
        }
    }
}
