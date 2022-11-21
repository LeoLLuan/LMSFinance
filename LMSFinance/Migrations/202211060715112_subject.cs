namespace LMSFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subject",
                c => new
                {
                    SubjectID = c.String(nullable: false, maxLength: 128),
                    SubjectName = c.String(),
                    FacultyName = c.String(),
                    Duration = c.Int(nullable: false),
                    Unit = c.String(),
                    PerUnit = c.Int(nullable: false),
                    TotalFee = c.Int(nullable: false),
                    Status = c.String(),
                })
                .PrimaryKey(t => t.SubjectID);

        }

        public override void Down()
        {
            DropTable("dbo.Subject");
        }
    }
}
