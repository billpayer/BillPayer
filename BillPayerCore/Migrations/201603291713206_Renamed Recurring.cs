namespace BillPayerCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedRecurring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bills", "Recurring", c => c.Boolean(nullable: false));
            DropColumn("dbo.Bills", "Recuring");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bills", "Recuring", c => c.Boolean(nullable: false));
            DropColumn("dbo.Bills", "Recurring");
        }
    }
}
