namespace InterviewSelectionProcess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phoneNo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Candidate", "MobileNumber", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Candidate", "MobileNumber", c => c.String(maxLength: 10, unicode: false));
        }
    }
}
