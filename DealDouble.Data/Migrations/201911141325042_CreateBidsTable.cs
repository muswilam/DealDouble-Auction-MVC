namespace DealDouble.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBidsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BidAmount = c.Double(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        AuctionId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auctions", t => t.AuctionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.AuctionId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bids", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bids", "AuctionId", "dbo.Auctions");
            DropIndex("dbo.Bids", new[] { "UserId" });
            DropIndex("dbo.Bids", new[] { "AuctionId" });
            DropTable("dbo.Bids");
        }
    }
}
