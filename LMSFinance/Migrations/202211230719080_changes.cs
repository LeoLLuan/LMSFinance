namespace LMSFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Receipt", "StudentID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Receipt", "StudentID");
        }
    }
}
