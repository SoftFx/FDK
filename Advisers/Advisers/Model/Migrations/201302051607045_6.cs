namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DoubleArbitrage2B1A",
                c => new
                    {
                        DoubleArbitrage2B1AId = c.Int(nullable: false, identity: true),
                        Symbol = c.String(),
                        ExpectedAsk = c.Double(nullable: false),
                        ExpectedAskVolume = c.Double(nullable: false),
                        ExecutedAsk = c.Double(nullable: false),
                        ExecutedAskVolume = c.Double(nullable: false),
                        ExecutedBid = c.Double(nullable: false),
                        ExecutedBidVolume = c.Double(nullable: false),
                        FirstArbitrage_ArbitrageId = c.Int(),
                        SecondArbitrage_ArbitrageId = c.Int(),
                    })
                .PrimaryKey(t => t.DoubleArbitrage2B1AId)
                .ForeignKey("dbo.Arbitrages", t => t.FirstArbitrage_ArbitrageId)
                .ForeignKey("dbo.Arbitrages", t => t.SecondArbitrage_ArbitrageId)
                .Index(t => t.FirstArbitrage_ArbitrageId)
                .Index(t => t.SecondArbitrage_ArbitrageId);
            
            CreateTable(
                "dbo.DoubleArbitrage1B2A",
                c => new
                    {
                        DoubleArbitrage1B2AId = c.Int(nullable: false, identity: true),
                        Symbol = c.String(),
                        ExpectedBid = c.Double(nullable: false),
                        ExpectedBidVolume = c.Double(nullable: false),
                        ExecutedBid = c.Double(nullable: false),
                        ExecutedBidVolume = c.Double(nullable: false),
                        ExecutedAsk = c.Double(nullable: false),
                        ExecutedBidAsk = c.Double(nullable: false),
                        FirstArbitrage_ArbitrageId = c.Int(),
                        SecondArbitrage_ArbitrageId = c.Int(),
                    })
                .PrimaryKey(t => t.DoubleArbitrage1B2AId)
                .ForeignKey("dbo.Arbitrages", t => t.FirstArbitrage_ArbitrageId)
                .ForeignKey("dbo.Arbitrages", t => t.SecondArbitrage_ArbitrageId)
                .Index(t => t.FirstArbitrage_ArbitrageId)
                .Index(t => t.SecondArbitrage_ArbitrageId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DoubleArbitrage1B2A", new[] { "SecondArbitrage_ArbitrageId" });
            DropIndex("dbo.DoubleArbitrage1B2A", new[] { "FirstArbitrage_ArbitrageId" });
            DropIndex("dbo.DoubleArbitrage2B1A", new[] { "SecondArbitrage_ArbitrageId" });
            DropIndex("dbo.DoubleArbitrage2B1A", new[] { "FirstArbitrage_ArbitrageId" });
            DropForeignKey("dbo.DoubleArbitrage1B2A", "SecondArbitrage_ArbitrageId", "dbo.Arbitrages");
            DropForeignKey("dbo.DoubleArbitrage1B2A", "FirstArbitrage_ArbitrageId", "dbo.Arbitrages");
            DropForeignKey("dbo.DoubleArbitrage2B1A", "SecondArbitrage_ArbitrageId", "dbo.Arbitrages");
            DropForeignKey("dbo.DoubleArbitrage2B1A", "FirstArbitrage_ArbitrageId", "dbo.Arbitrages");
            DropTable("dbo.DoubleArbitrage1B2A");
            DropTable("dbo.DoubleArbitrage2B1A");
        }
    }
}
