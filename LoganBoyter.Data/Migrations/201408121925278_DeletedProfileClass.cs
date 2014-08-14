namespace LoganBoyter.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedProfileClass : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Profiles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FacebookId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
