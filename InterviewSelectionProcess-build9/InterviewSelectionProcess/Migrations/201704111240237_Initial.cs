namespace InterviewSelectionProcess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidate",
                c => new
                    {
                        CandidateID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50, unicode: false),
                        MiddleName = c.String(nullable: false, maxLength: 50, unicode: false),
                        LastName = c.String(nullable: false, maxLength: 50, unicode: false),
                        DateOfBirth = c.DateTime(),
                        Address = c.String(maxLength: 200, unicode: false),
                        Experience = c.Int(nullable: false),
                        TechnologiesWorkedOnID = c.Int(nullable: false),
                        PostID = c.Int(nullable: false),
                        MobileNumber = c.String(maxLength: 50, unicode: false),
                        EmailAddress = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.CandidateID)
                .ForeignKey("dbo.Post", t => t.PostID)
                .ForeignKey("dbo.Technologies", t => t.TechnologiesWorkedOnID)
                .Index(t => t.TechnologiesWorkedOnID)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.CandidateStatus",
                c => new
                    {
                        CandidateStatusID = c.Int(nullable: false, identity: true),
                        CandidateID = c.Int(nullable: false),
                        InterviewStatus = c.Int(nullable: false),
                        SelectionStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CandidateStatusID)
                .ForeignKey("dbo.InterviewStatus", t => t.InterviewStatus)
                .ForeignKey("dbo.SelectionStatus", t => t.SelectionStatus)
                .ForeignKey("dbo.Candidate", t => t.CandidateID)
                .Index(t => t.CandidateID)
                .Index(t => t.InterviewStatus)
                .Index(t => t.SelectionStatus);
            
            CreateTable(
                "dbo.InterviewStatus",
                c => new
                    {
                        InterviewStatusID = c.Int(nullable: false, identity: true),
                        InterviewStatusName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.InterviewStatusID);
            
            CreateTable(
                "dbo.SelectionStatus",
                c => new
                    {
                        SelectionStatusID = c.Int(nullable: false, identity: true),
                        SelectionStatusName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.SelectionStatusID);
            
            CreateTable(
                "dbo.InterviewSchedule",
                c => new
                    {
                        InterviewID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        HRID = c.Int(),
                        Note = c.String(maxLength: 500, unicode: false),
                        InterviewDate = c.DateTime(nullable: false),
                        CandidateID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InterviewID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .ForeignKey("dbo.Candidate", t => t.CandidateID)
                .Index(t => t.EmployeeID)
                .Index(t => t.CandidateID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        EmailID = c.String(nullable: false, maxLength: 50, unicode: false),
                        Role = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        PostName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.PostID);
            
            CreateTable(
                "dbo.Technologies",
                c => new
                    {
                        TechID = c.Int(nullable: false, identity: true),
                        Technology = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.TechID);
            
            CreateTable(
                "dbo.TechStack",
                c => new
                    {
                        TeckStackID = c.Int(nullable: false, identity: true),
                        CandidateID = c.String(nullable: false, maxLength: 50, unicode: false),
                        TechID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeckStackID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Candidate", "TechnologiesWorkedOnID", "dbo.Technologies");
            DropForeignKey("dbo.Candidate", "PostID", "dbo.Post");
            DropForeignKey("dbo.InterviewSchedule", "CandidateID", "dbo.Candidate");
            DropForeignKey("dbo.InterviewSchedule", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.CandidateStatus", "CandidateID", "dbo.Candidate");
            DropForeignKey("dbo.CandidateStatus", "SelectionStatus", "dbo.SelectionStatus");
            DropForeignKey("dbo.CandidateStatus", "InterviewStatus", "dbo.InterviewStatus");
            DropIndex("dbo.InterviewSchedule", new[] { "CandidateID" });
            DropIndex("dbo.InterviewSchedule", new[] { "EmployeeID" });
            DropIndex("dbo.CandidateStatus", new[] { "SelectionStatus" });
            DropIndex("dbo.CandidateStatus", new[] { "InterviewStatus" });
            DropIndex("dbo.CandidateStatus", new[] { "CandidateID" });
            DropIndex("dbo.Candidate", new[] { "PostID" });
            DropIndex("dbo.Candidate", new[] { "TechnologiesWorkedOnID" });
            DropTable("dbo.TechStack");
            DropTable("dbo.Technologies");
            DropTable("dbo.Post");
            DropTable("dbo.Employees");
            DropTable("dbo.InterviewSchedule");
            DropTable("dbo.SelectionStatus");
            DropTable("dbo.InterviewStatus");
            DropTable("dbo.CandidateStatus");
            DropTable("dbo.Candidate");
        }
    }
}
