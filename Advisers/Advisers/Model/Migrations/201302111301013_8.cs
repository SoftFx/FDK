namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArbitrageDetails", "B1Ask1Volume", c => c.Double(nullable: false));
            AddColumn("dbo.ArbitrageDetails", "B1Bid1Volume", c => c.Double(nullable: false));
            AddColumn("dbo.ArbitrageDetails", "B2Ask1Volume", c => c.Double(nullable: false));
            AddColumn("dbo.ArbitrageDetails", "B2Bid1Volume", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ArbitrageDetails", "B2Bid1Volume");
            DropColumn("dbo.ArbitrageDetails", "B2Ask1Volume");
            DropColumn("dbo.ArbitrageDetails", "B1Bid1Volume");
            DropColumn("dbo.ArbitrageDetails", "B1Ask1Volume");
        }
    }
}
