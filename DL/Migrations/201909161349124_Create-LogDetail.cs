namespace DL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateLogDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        StackTrace = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogDetails");
        }
    }
}
