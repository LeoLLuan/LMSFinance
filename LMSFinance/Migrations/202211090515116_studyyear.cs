namespace LMSFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studyyear : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudyYear",
                c => new
                    {
                        SchoolYear = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.SchoolYear);
        }
        
        public override void Down()
        {
            DropTable("dbo.StudyYear");
        }
    }
}
