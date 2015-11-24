namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Arbitrages", "ArbitrageSpecificDAId", c => c.Int());
            AddColumn("dbo.Arbitrages", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.DoubleArbitrage2B1A", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DoubleArbitrage2B1A", "TradeDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DoubleArbitrage1B2A", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DoubleArbitrage1B2A", "TradeDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DoubleArbitrage1B2A", "TradeDate");
            DropColumn("dbo.DoubleArbitrage1B2A", "CreatedDate");
            DropColumn("dbo.DoubleArbitrage2B1A", "TradeDate");
            DropColumn("dbo.DoubleArbitrage2B1A", "CreatedDate");
            DropColumn("dbo.Arbitrages", "Discriminator");
            DropColumn("dbo.Arbitrages", "ArbitrageSpecificDAId");
        }
    }
}
