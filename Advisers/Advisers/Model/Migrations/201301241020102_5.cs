namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Arbitrages", "Symbol", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Arbitrages", "Symbol");
        }
    }
}
