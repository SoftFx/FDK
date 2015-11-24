namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Arbitrages",
                c => new
                    {
                        ArbitrageId = c.Int(nullable: false, identity: true),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                        BankBidGAsk = c.Int(nullable: false),
                        BankAskLBid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ArbitrageId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Arbitrages");
        }
    }
}
