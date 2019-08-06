namespace PMY.CompilationSystem.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _181221change2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "LastCompileTime", c => c.DateTime());
            AlterColumn("dbo.Projects", "LatestModifiTime", c => c.DateTime());
            DropColumn("dbo.Projects", "SVNFoldersId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "SVNFoldersId", c => c.Int(nullable: false));
            AlterColumn("dbo.Projects", "LatestModifiTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Projects", "LastCompileTime", c => c.DateTime(nullable: false));
        }
    }
}
