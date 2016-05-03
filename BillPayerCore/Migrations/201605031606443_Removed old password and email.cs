namespace BillPayerCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removedoldpasswordandemail : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Email");
            DropColumn("dbo.Users", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Password", c => c.String());
            AddColumn("dbo.Users", "Email", c => c.String());
        }
    }
}
