namespace InterviewSelectionProcess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class note_string_change : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InterviewSchedule", "HRNote", c => c.String(maxLength: 500));
            DropColumn("dbo.InterviewSchedule", "HRID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InterviewSchedule", "HRID", c => c.Int());
            DropColumn("dbo.InterviewSchedule", "HRNote");
        }
    }
}
