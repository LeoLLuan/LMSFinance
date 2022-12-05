namespace LMSFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Discount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Discount",
                c => new
                    {
                        Discounts = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Discounts);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Discount");
        }
    }
}
