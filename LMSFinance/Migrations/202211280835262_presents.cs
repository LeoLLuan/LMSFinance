namespace LMSFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class presents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Present",
                c => new
                    {
                        Presents = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Presents);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Present");
        }
    }
}
