namespace DealDouble.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoriesAndCommentsTablesDataAnnotaions : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 125));
            AlterColumn("dbo.Categories", "Description", c => c.String(maxLength: 400));
            AlterColumn("dbo.Comments", "Body", c => c.String(nullable: false, maxLength: 400));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Body", c => c.String());
            AlterColumn("dbo.Categories", "Description", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
        }
    }
}
