namespace LoganBoyter.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedForeignKeyRelationshipInComments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "ProfileId", "dbo.Profiles");
            DropIndex("dbo.Comments", new[] { "ProfileId" });
            AddColumn("dbo.Comments", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comments", "User_Id");
            AddForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Comments", "ProfileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "ProfileId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropColumn("dbo.Comments", "User_Id");
            DropColumn("dbo.Comments", "UserId");
            CreateIndex("dbo.Comments", "ProfileId");
            AddForeignKey("dbo.Comments", "ProfileId", "dbo.Profiles", "Id", cascadeDelete: true);
        }
    }
}
