namespace LMSFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class grade : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grade",
                c => new
                {
                    GradeName = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.GradeName);
        }
        
        public override void Down()
        {
            DropTable("dbo.Grade");
        }
    }
}
