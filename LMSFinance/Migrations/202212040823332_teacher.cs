namespace LMSFinance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teacher : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        TeacherId = c.String(nullable: false, maxLength: 128),
                        TeacherName = c.String(),
                        Code = c.String(),
                        Role = c.String(),
                        ContractType = c.String(),
                        StartingDate = c.DateTime(nullable: false),
                        LeaveDay = c.String(),
                        Status = c.String(),
                        Gender = c.String(),
                        Birthday = c.String(),
                        IdentityNo = c.String(),
                        IssuedBy = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.TeacherId);
        }
        
        public override void Down()
        {
            DropTable("dbo.Teacher");
        }
    }
}
