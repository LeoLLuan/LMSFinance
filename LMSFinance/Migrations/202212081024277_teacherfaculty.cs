namespace LMSFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teacherfaculty : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeaFaculty",
                c => new
                    {
                        TeaFaculty = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.TeaFaculty);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TeaFaculty");
        }
    }
}
