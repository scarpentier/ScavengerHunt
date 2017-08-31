namespace ScavengerHunt.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Value = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
            CreateTable(
                "dbo.Stunts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaxScore = c.Int(nullable: false),
                        Keyword = c.String(),
                        Type = c.Int(nullable: false),
                        Published = c.Boolean(nullable: false),
                        JudgeNotes = c.String(),
                        Collapsible = c.Boolean(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeamStunts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        Submission = c.String(),
                        TeamNotes = c.String(),
                        JudgeFeedback = c.String(),
                        JudgeNotes = c.String(),
                        Status = c.Int(nullable: false),
                        Team_Id = c.Int(nullable: false),
                        Stunt_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .ForeignKey("dbo.Stunts", t => t.Stunt_Id, cascadeDelete: true)
                .Index(t => t.Team_Id)
                .Index(t => t.Stunt_Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Token = c.String(),
                        Tagline = c.String(),
                        Url = c.String(),
                        BonusPoints = c.Int(nullable: false),
                        ContactUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ContactUser_Id)
                .Index(t => t.ContactUser_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Email = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Teams", t => t.Team_Id)
                .Index(t => t.Team_Id);
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.StuntTranslations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Language = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        Stunt_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stunts", t => t.Stunt_Id, cascadeDelete: true)
                .Index(t => t.Stunt_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StuntTranslations", "Stunt_Id", "dbo.Stunts");
            DropForeignKey("dbo.TeamStunts", "Stunt_Id", "dbo.Stunts");
            DropForeignKey("dbo.TeamStunts", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Users", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Teams", "ContactUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropIndex("dbo.StuntTranslations", new[] { "Stunt_Id" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "Team_Id" });
            DropIndex("dbo.Teams", new[] { "ContactUser_Id" });
            DropIndex("dbo.TeamStunts", new[] { "Stunt_Id" });
            DropIndex("dbo.TeamStunts", new[] { "Team_Id" });
            DropTable("dbo.StuntTranslations");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Teams");
            DropTable("dbo.TeamStunts");
            DropTable("dbo.Stunts");
            DropTable("dbo.Settings");
            DropTable("dbo.Roles");
        }
    }
}
