namespace TasksApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskAndTagEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(nullable: false, maxLength: 20),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);

            CreateTable(
                "dbo.Tasks",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Title = c.String(nullable: false, maxLength: 30),
                    Content = c.String(nullable: false, maxLength: 500),
                    IsDone = c.Boolean(nullable: false),
                    CreationDate = c.DateTime(nullable: false),
                    LastModificationDate = c.DateTime(nullable: false),
                    ExpirationDate = c.DateTime(),
                    User_Id = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Title)
                .Index(t => t.CreationDate)
                .Index(t => t.User_Id);

            CreateTable(
                "dbo.TasksTags",
                c => new
                {
                    TaskId = c.Guid(nullable: false),
                    TagId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => new { t.TaskId, t.TagId })
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.TaskId)
                .Index(t => t.TagId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TasksTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.TasksTags", "TaskId", "dbo.Tasks");
            DropIndex("dbo.TasksTags", new[] { "TagId" });
            DropIndex("dbo.TasksTags", new[] { "TaskId" });
            DropIndex("dbo.Tasks", new[] { "User_Id" });
            DropIndex("dbo.Tasks", new[] { "CreationDate" });
            DropIndex("dbo.Tasks", new[] { "Title" });
            DropIndex("dbo.Tags", new[] { "Name" });
            DropTable("dbo.TasksTags");
            DropTable("dbo.Tasks");
            DropTable("dbo.Tags");
        }
    }
}
