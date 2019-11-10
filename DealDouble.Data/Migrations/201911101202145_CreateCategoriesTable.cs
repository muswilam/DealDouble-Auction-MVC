namespace DealDouble.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCategoriesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Auctions", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Auctions", "CategoryId");
            AddForeignKey("dbo.Auctions", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Auctions", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Auctions", new[] { "CategoryId" });
            DropColumn("dbo.Auctions", "CategoryId");
            DropTable("dbo.Categories");
        }
    }
}
