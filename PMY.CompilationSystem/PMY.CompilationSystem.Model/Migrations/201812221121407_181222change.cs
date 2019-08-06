namespace PMY.CompilationSystem.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _181222change : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassName = c.String(nullable: false, maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Projects", "ProjectClassName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Projects", "SVNFolderName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "SVNFolderName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Projects", "ProjectClassName");
            DropTable("dbo.ProjectClasses");
        }
    }
}
