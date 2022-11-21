namespace LMSFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class receipt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Receipt",
                c => new
                    {
                        NO = c.Int(nullable: false, identity: true),
                        TotalDuration = c.Int(nullable: false),
                        FeePerUnit = c.Int(nullable: false),
                        TotalSchoolFee = c.Int(nullable: false),
                        CollectMethod = c.String(),
                        DiscountObject = c.String(),
                        Sale = c.Boolean(nullable: false),
                        SaleType = c.String(),
                        TotalSubFeeCheck = c.Boolean(nullable: false),
                        TotalSubFee = c.Int(nullable: false),
                        SubFeeDescription = c.String(),
                        CollectedDate = c.DateTime(nullable: false),
                        Money = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NO);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Receipt");
        }
    }
}
