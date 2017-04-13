namespace InterviewSelectionProcess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_NewColumn_ResumePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidate", "ResumePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidate", "ResumePath");
        }
    }
}
