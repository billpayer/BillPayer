namespace BillPayerCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applicationuser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateDue = c.DateTime(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Recuring = c.Boolean(nullable: false),
                        Paid = c.Boolean(nullable: false),
                        HouseHold_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HouseHolds", t => t.HouseHold_Id)
                .Index(t => t.HouseHold_Id);
            
            CreateTable(
                "dbo.BillSplits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PortionCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        User_Id = c.Int(),
                        Bill_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Bills", t => t.Bill_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Bill_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Birthday = c.DateTime(),
                        Age = c.Int(nullable: false),
                        Email = c.String(),
                        Password = c.String(),
                        Sex = c.String(),
                        HouseHold_Id = c.Int(),
                        HouseHold_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HouseHolds", t => t.HouseHold_Id)
                .ForeignKey("dbo.HouseHolds", t => t.HouseHold_Id1)
                .Index(t => t.HouseHold_Id)
                .Index(t => t.HouseHold_Id1);
            
            CreateTable(
                "dbo.HouseHolds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Size = c.Single(nullable: false),
                        Rooms = c.Int(nullable: false),
                        Bathrooms = c.Single(nullable: false),
                        Address = c.String(),
                        HeadOfHouseHold_Id = c.Int(),
                        User_Id = c.Int(),
                        User_Id1 = c.Int(),
                        User_Id2 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.HeadOfHouseHold_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.User_Id1)
                .ForeignKey("dbo.Users", t => t.User_Id2)
                .Index(t => t.HeadOfHouseHold_Id)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1)
                .Index(t => t.User_Id2);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                        UserInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserInfo_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.UserInfo_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "UserInfo_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.BillSplits", "Bill_Id", "dbo.Bills");
            DropForeignKey("dbo.BillSplits", "User_Id", "dbo.Users");
            DropForeignKey("dbo.HouseHolds", "User_Id2", "dbo.Users");
            DropForeignKey("dbo.HouseHolds", "User_Id1", "dbo.Users");
            DropForeignKey("dbo.HouseHolds", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "HouseHold_Id1", "dbo.HouseHolds");
            DropForeignKey("dbo.Users", "HouseHold_Id", "dbo.HouseHolds");
            DropForeignKey("dbo.HouseHolds", "HeadOfHouseHold_Id", "dbo.Users");
            DropForeignKey("dbo.Bills", "HouseHold_Id", "dbo.HouseHolds");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "UserInfo_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.HouseHolds", new[] { "User_Id2" });
            DropIndex("dbo.HouseHolds", new[] { "User_Id1" });
            DropIndex("dbo.HouseHolds", new[] { "User_Id" });
            DropIndex("dbo.HouseHolds", new[] { "HeadOfHouseHold_Id" });
            DropIndex("dbo.Users", new[] { "HouseHold_Id1" });
            DropIndex("dbo.Users", new[] { "HouseHold_Id" });
            DropIndex("dbo.BillSplits", new[] { "Bill_Id" });
            DropIndex("dbo.BillSplits", new[] { "User_Id" });
            DropIndex("dbo.Bills", new[] { "HouseHold_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.HouseHolds");
            DropTable("dbo.Users");
            DropTable("dbo.BillSplits");
            DropTable("dbo.Bills");
        }
    }
}
