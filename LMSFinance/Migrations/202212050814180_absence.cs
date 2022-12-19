namespace LMSFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class absence : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Absence",
                c => new
                    {
                        NO = c.Int(nullable: false, identity: true),
                        TeacherId = c.String(),
                        TName = c.String(),
                        Faculty = c.String(),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        Reason = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.NO);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Absence");
        }
    }
}
