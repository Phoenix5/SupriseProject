namespace Interview.Domain.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Authentication : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidate",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        MiddleName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DateOfBirth = c.DateTime(),
                        Address = c.String(maxLength: 200),
                        Experience = c.Int(nullable: false),
                        TechnologyID = c.Int(nullable: false),
                        PostID = c.Int(nullable: false),
                        MobileNumber = c.String(nullable: false),
                        ResumePath = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ID)
                .ForeignKey("dbo.Post", t => t.PostID, cascadeDelete: true)
                .ForeignKey("dbo.Technologies", t => t.TechnologyID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TechnologyID)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.CandidateStatus",
                c => new
                    {
                        CandidateStatusID = c.Int(nullable: false, identity: true),
                        CandidateID = c.String(maxLength: 128),
                        InterviewStatusID = c.Int(nullable: false),
                        SelectionStatusID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CandidateStatusID)
                .ForeignKey("dbo.Candidate", t => t.CandidateID)
                .ForeignKey("dbo.InterviewStatus", t => t.InterviewStatusID, cascadeDelete: true)
                .ForeignKey("dbo.SelectionStatus", t => t.SelectionStatusID, cascadeDelete: true)
                .Index(t => t.CandidateID)
                .Index(t => t.InterviewStatusID)
                .Index(t => t.SelectionStatusID);
            
            CreateTable(
                "dbo.InterviewStatus",
                c => new
                    {
                        InterviewStatusID = c.Int(nullable: false, identity: true),
                        InterviewStatusName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.InterviewStatusID);
            
            CreateTable(
                "dbo.SelectionStatus",
                c => new
                    {
                        SelectionStatusID = c.Int(nullable: false, identity: true),
                        SelectionStatusName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.SelectionStatusID);
            
            CreateTable(
                "dbo.InterviewSchedule",
                c => new
                    {
                        InterviewID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.String(maxLength: 128),
                        HRNote = c.String(maxLength: 500),
                        Note = c.String(maxLength: 500),
                        InterviewDate = c.DateTime(nullable: false),
                        CandidateID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.InterviewID)
                .ForeignKey("dbo.Candidate", t => t.CandidateID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .Index(t => t.EmployeeID)
                .Index(t => t.CandidateID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        PostName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.PostID);
            
            CreateTable(
                "dbo.Technologies",
                c => new
                    {
                        TechID = c.Int(nullable: false, identity: true),
                        TechnologyName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.TechID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.TechStack",
                c => new
                    {
                        TeckStackID = c.Int(nullable: false, identity: true),
                        CandidateID = c.String(nullable: false, maxLength: 50),
                        TechID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeckStackID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Candidate", "TechnologyID", "dbo.Technologies");
            DropForeignKey("dbo.Candidate", "PostID", "dbo.Post");
            DropForeignKey("dbo.InterviewSchedule", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Employees", "ID", "dbo.AspNetUsers");
            DropForeignKey("dbo.InterviewSchedule", "CandidateID", "dbo.Candidate");
            DropForeignKey("dbo.CandidateStatus", "SelectionStatusID", "dbo.SelectionStatus");
            DropForeignKey("dbo.CandidateStatus", "InterviewStatusID", "dbo.InterviewStatus");
            DropForeignKey("dbo.CandidateStatus", "CandidateID", "dbo.Candidate");
            DropForeignKey("dbo.Candidate", "ID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Employees", new[] { "ID" });
            DropIndex("dbo.InterviewSchedule", new[] { "CandidateID" });
            DropIndex("dbo.InterviewSchedule", new[] { "EmployeeID" });
            DropIndex("dbo.CandidateStatus", new[] { "SelectionStatusID" });
            DropIndex("dbo.CandidateStatus", new[] { "InterviewStatusID" });
            DropIndex("dbo.CandidateStatus", new[] { "CandidateID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Candidate", new[] { "PostID" });
            DropIndex("dbo.Candidate", new[] { "TechnologyID" });
            DropIndex("dbo.Candidate", new[] { "ID" });
            DropTable("dbo.TechStack");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Technologies");
            DropTable("dbo.Post");
            DropTable("dbo.Employees");
            DropTable("dbo.InterviewSchedule");
            DropTable("dbo.SelectionStatus");
            DropTable("dbo.InterviewStatus");
            DropTable("dbo.CandidateStatus");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Candidate");
        }
    }
}
