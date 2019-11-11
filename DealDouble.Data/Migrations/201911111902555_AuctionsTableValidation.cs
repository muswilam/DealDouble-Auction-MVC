namespace DealDouble.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuctionsTableValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Auctions", "Title", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Auctions", "StartingTime", c => c.DateTime());
            AlterColumn("dbo.Auctions", "EndingTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Auctions", "EndingTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Auctions", "StartingTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Auctions", "Title", c => c.String());
        }
    }
}
