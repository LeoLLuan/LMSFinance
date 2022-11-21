namespace LMSFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subjectdetail : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SubjectDetail", "SubjectID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubjectDetail", "SubjectID", c => c.String());
        }
    }
}
