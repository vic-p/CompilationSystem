namespace PMY.CompilationSystem.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SVNFoldersId = c.Int(nullable: false),
                        Sort = c.Double(nullable: false),
                        SVNFolderName = c.String(nullable: false, maxLength: 50),
                        ProjectName = c.String(nullable: false, maxLength: 50),
                        ProjectPath = c.String(nullable: false, maxLength: 100),
                        LastCompileTime = c.DateTime(nullable: false),
                        LatestModifiTime = c.DateTime(nullable: false),
                        PublishPath = c.String(nullable: false, maxLength: 100),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SVNFolders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FolderName = c.String(nullable: false, maxLength: 50),
                        FolderPath = c.String(nullable: false, maxLength: 100),
                        Sort = c.Double(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TaskLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActionType = c.String(nullable: false, maxLength: 50),
                        State = c.Int(nullable: false),
                        ActionPath = c.String(nullable: false, maxLength: 100),
                        CreateTime = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        Creator = c.String(nullable: false, maxLength: 50),
                        FinishTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TaskLists");
            DropTable("dbo.SVNFolders");
            DropTable("dbo.Projects");
        }
    }
}
