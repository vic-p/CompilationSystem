namespace PMY.CompilationSystem.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _181223change : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "ProjectParentPath", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "ProjectParentPath");
        }
    }
}
