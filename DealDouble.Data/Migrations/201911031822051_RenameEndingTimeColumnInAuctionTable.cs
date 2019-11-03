namespace DealDouble.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameEndingTimeColumnInAuctionTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "EndingTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Auctions", "EndTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Auctions", "EndTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Auctions", "EndingTime");
        }
    }
}
