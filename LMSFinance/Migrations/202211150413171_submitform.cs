namespace LMSFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class submitform : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubmitForm",
                c => new
                    {
                        NO = c.Int(nullable: false, identity: true),
                        StudentId = c.String(nullable: false),
                        SubjectName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.NO);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SubmitForm");
        }
    }
}
