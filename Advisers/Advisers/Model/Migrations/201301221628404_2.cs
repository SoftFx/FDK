namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Arbitrages", "Duration", c => c.Double(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B1Ask1", c => c.Double(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B1Ask2", c => c.Double(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B1Ask3", c => c.Double(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B1Bid1", c => c.Double(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B1Bid2", c => c.Double(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B1Bid3", c => c.Double(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B2Ask1", c => c.Double(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B2Ask2", c => c.Double(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B2Ask3", c => c.Double(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B2Bid1", c => c.Double(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B2Bid2", c => c.Double(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B2Bid3", c => c.Double(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "spread", c => c.Double(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "duration", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ArbitrageDetails", "duration", c => c.Int(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "spread", c => c.Int(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B2Bid3", c => c.Int(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B2Bid2", c => c.Int(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B2Bid1", c => c.Int(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B2Ask3", c => c.Int(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B2Ask2", c => c.Int(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B2Ask1", c => c.Int(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B1Bid3", c => c.Int(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B1Bid2", c => c.Int(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B1Bid1", c => c.Int(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B1Ask3", c => c.Int(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B1Ask2", c => c.Int(nullable: false));
            AlterColumn("dbo.ArbitrageDetails", "B1Ask1", c => c.Int(nullable: false));
            AlterColumn("dbo.Arbitrages", "Duration", c => c.Int(nullable: false));
        }
    }
}
