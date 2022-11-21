namespace LMSFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class faculty : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Faculty",
                c => new
                {
                    FacultyName = c.String(nullable: false, maxLength: 128), 
                })
                .PrimaryKey(t => t.FacultyName);
        }
        
        public override void Down()
        {
            DropTable("dbo.Subject");
        }
    }
}
