namespace DL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateVoteRep : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        Votes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "BookId", "dbo.Books");
            DropIndex("dbo.Votes", new[] { "BookId" });
            DropTable("dbo.Votes");
        }
    }
}
