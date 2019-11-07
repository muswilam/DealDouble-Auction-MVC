namespace DealDouble.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAuctionPicturesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuctionPictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuctionId = c.Int(nullable: false),
                        PictureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: true)
                .ForeignKey("dbo.Auctions", t => t.AuctionId, cascadeDelete: true)
                .Index(t => t.PictureId)
                .Index(t => t.AuctionId);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Auctions", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Auctions", "ImageUrl", c => c.String());
            DropForeignKey("dbo.AuctionPictures", "AuctionId", "dbo.Auctions");
            DropForeignKey("dbo.AuctionPictures", "PictureId", "dbo.Pictures");
            DropIndex("dbo.AuctionPictures", new[] { "AuctionId" });
            DropIndex("dbo.AuctionPictures", new[] { "PictureId" });
            DropTable("dbo.Pictures");
            DropTable("dbo.AuctionPictures");
        }
    }
}
