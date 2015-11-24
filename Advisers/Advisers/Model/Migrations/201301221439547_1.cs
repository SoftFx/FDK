namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArbitrageDetails",
                c => new
                    {
                        ArbitrageDetailId = c.Int(nullable: false, identity: true),
                        B1Ask1 = c.Int(nullable: false),
                        B1Ask2 = c.Int(nullable: false),
                        B1Ask3 = c.Int(nullable: false),
                        B1Bid1 = c.Int(nullable: false),
                        B1Bid2 = c.Int(nullable: false),
                        B1Bid3 = c.Int(nullable: false),
                        B2Ask1 = c.Int(nullable: false),
                        B2Ask2 = c.Int(nullable: false),
                        B2Ask3 = c.Int(nullable: false),
                        B2Bid1 = c.Int(nullable: false),
                        B2Bid2 = c.Int(nullable: false),
                        B2Bid3 = c.Int(nullable: false),
                        spread = c.Int(nullable: false),
                        duration = c.Int(nullable: false),
                        StartTickTime = c.DateTime(nullable: false, storeType: "datetime2"),
                        EndTickTime = c.DateTime(nullable: false, storeType: "datetime2"),
                        Arbitrage_ArbitrageId = c.Int(),
                    })
                .PrimaryKey(t => t.ArbitrageDetailId)
                .ForeignKey("dbo.Arbitrages", t => t.Arbitrage_ArbitrageId)
                .Index(t => t.Arbitrage_ArbitrageId);
            
            AlterColumn("dbo.Arbitrages", "StartDateTime", c => c.DateTime(nullable: false, storeType: "datetime2"));
            AlterColumn("dbo.Arbitrages", "EndDateTime", c => c.DateTime(nullable: false, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropIndex("dbo.ArbitrageDetails", new[] { "Arbitrage_ArbitrageId" });
            DropForeignKey("dbo.ArbitrageDetails", "Arbitrage_ArbitrageId", "dbo.Arbitrages");
            AlterColumn("dbo.Arbitrages", "EndDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Arbitrages", "StartDateTime", c => c.DateTime(nullable: false));
            DropTable("dbo.ArbitrageDetails");
        }
    }
}
